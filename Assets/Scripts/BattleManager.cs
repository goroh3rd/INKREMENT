using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private CardManager cardManager;
    public CardManager CardManager => cardManager;
    private Player player;
    public Player Player => player;
    private BattleState state;
    public BattleState State => state;
    public event Action OnBattleStart;
    public event Action<BattleState, BattleState> OnStateChange;
    public event Action OnBattleEnd;
    public void StartBattle()
    {
        state = BattleState.Start;
        if (player == null)
        {
            player = new Player();
            player.health = new Player.PlayerHealth(10, 10, 10, 0); // ここの値は適当
        }
        cardManager = new CardManager();
        cardManager.drawPile = new List<Cards>(CardManager.deck);
        cardManager.Shuffle(cardManager.drawPile);
        cardManager.hand = new List<Cards>();
        cardManager.discardPile = new List<Cards>();
        cardManager.DrawCards(5); // ここの値は適当
        OnBattleStart?.Invoke();
        ChangeState(BattleState.PlayerTurn);
    }
    public void EndTurn()
    {
        if (state != BattleState.PlayerTurn) return;
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
        StartTurn();
    }
    private void EndBattle()
    {
        OnBattleEnd?.Invoke();
    }
}
