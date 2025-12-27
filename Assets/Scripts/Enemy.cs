using System;
using UnityEngine;

public abstract class Enemy : IDamageable
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
    private readonly EnemyStats initialStats;
    public EnemyStats InitialStats => initialStats;
    private EnemyStats currentStats;
    public EnemyStats CurrentStats => currentStats;
    public Enemy(EnemyStats stats)
    {
        initialStats = stats.Clone();
        currentStats = stats.Clone();
    }
    public void TakeDamage(int damage)
    {
        currentStats.maxHealth -= damage;
    }
    public abstract void OnAction();
    public bool IsDefeated() { return currentStats.maxHealth <= 0; }
    public abstract void OnDamageTaken(int damage, int currentHealth);
}