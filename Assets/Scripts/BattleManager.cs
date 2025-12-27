using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public enum BattleState
    {
        Start,
        PlayerTurn,
        EnemyTurn,
        Won,
        Lost
    }
    [Serializable]
    private class Refs
    {
        [SerializeField] HealthBar healthBar;
        public HealthBar HealthBar => healthBar;
        [SerializeField] private RectTransform cardArea;
        public RectTransform CardArea => cardArea;
        [SerializeField] private CardInterface cardInterfacePrefab;
        public CardInterface CardInterfacePrefab => cardInterfacePrefab;
    }
    [SerializeField] private Refs refs;
    private CardManager cardManager;
    public CardManager CardManager => cardManager;
    private Player player;
    public Player Player => player;
    public List<Enemy> enemies;
    private BattleState state;
    public BattleState State => state;
    public event Action OnBattleStart;
    public event Action<BattleState, BattleState> OnStateChange;
    public event Action OnBattleEnd;
    public void Init()
    {
        cardManager = new CardManager(this);
    }
    public void StartBattle()
    {
        state = BattleState.Start;
        if (player == null)
        {
            player = new Player();
        }
        enemies.ForEach(e => e.Init(this));
        refs.HealthBar.Init(player);
        OnBattleStart?.Invoke();
        ChangeState(BattleState.PlayerTurn);
    }
    public void EndTurn()
    {
        if (state != BattleState.PlayerTurn) return;
        cardManager.hand.ForEach(card => cardManager.DiscardCard(card));
        ChangeState(BattleState.EnemyTurn);
        SolveEnemyTurn();
    }
    public void StartTurn()
    {
        if (state != BattleState.EnemyTurn) return;
        ChangeState(BattleState.PlayerTurn);
    }
    private void ChangeState(BattleState newState)
    {
        BattleState previousState = state;
        state = newState;
        OnStateChange?.Invoke(previousState, newState);
        if (state == BattleState.Won || state == BattleState.Lost)
        {
            EndBattle();
        }
    }
    public void PlayCard(Cards card)
    {
        if (state != BattleState.PlayerTurn) return;
        if (cardManager.hand.Contains(card))
        {
            card.OnPlay();
            cardManager.hand.Remove(card);
            cardManager.discardPile.Add(card);
            // ターン終了の判定などをここで行う
        }
    }
    private void SolveEnemyTurn()
    {
        // 敵の行動ロジックをここに実装
        // 例: プレイヤーにダメージを与えるなど
        foreach (var enemy in enemies)
        {
            if (!enemy.IsDefeated())
            {
                enemy.OnAction();
            }
        }
        StartTurn();
    }
    private void EndBattle()
    {
        OnBattleEnd?.Invoke();
    }
    [ContextMenu("TestStartBattle")]
    private void TestStartBattle()
    {
        Init();
        for (int i = 0; i < 10; i++)
        {
            cardManager.AddToDeck(new Resist(this, Instantiate(refs.CardInterfacePrefab)));
        }
        StartBattle();
    }
}
