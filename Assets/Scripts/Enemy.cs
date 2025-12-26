using UnityEngine;

public abstract class Enemy : IDamageable
{
    private int health;
    public int Health
    {
        get { return health; }
        set { health = value <= 0 ? 0 : value; }
    }
    public Enemy(int initialHealth)
    {
        Health = initialHealth;
    }
    public void TakeDamage(int damage)
    {
        Health -= damage;
    }
    public abstract void OnAction();
    public bool IsDefeated() { return Health <= 0; }
    public abstract void OnDamageTaken(int damage, int currentHealth);
}