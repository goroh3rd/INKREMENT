using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    [Serializable]
    public struct EnemyStats
    {
        public string enemyName;
        public int maxHealth;
        public int attackPower;
        public EnemyStats(string name, int health, int attack)
        {
            enemyName = name;
            maxHealth = health;
            attackPower = attack;
        }
        public EnemyStats Clone()
        {
            return new EnemyStats(enemyName, maxHealth, attackPower);
        }
    }
    [SerializeField] private readonly EnemyStats initialStats;
    public EnemyStats InitialStats => initialStats;
    [SerializeField] private EnemyStats currentStats;
    public EnemyStats CurrentStats => currentStats;
    [SerializeField] protected EnemyUIElement uiElement;
    protected BattleManager battleManager;
    public void Init(BattleManager bm, EnemyStats stats)
    {
        currentStats = stats;
        battleManager = bm;
    }
    protected abstract void UpdatePredictUI();
    public void TakeDamage(int damage)
    {
        currentStats.maxHealth -= damage;
        OnDamageTaken(damage, currentStats.maxHealth);
        UpdatePredictUI();
    }
    public void UpdateStats(EnemyStats newStats)
    {
        currentStats = newStats;
    }
    public abstract void OnAction();
    public bool IsDefeated() { return currentStats.maxHealth <= 0; }
    public abstract void OnDamageTaken(int damage, int currentHealth);
}