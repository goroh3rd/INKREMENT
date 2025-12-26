using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : IDamageable
{
    [Serializable]
    public class PlayerHealth
    {
        private int c;
        private int m;
        private int y;
        private int k;
        public int C
        {
            get { return c; }
            set { c = value <= 0 ? 0 : value; }
        }
        public int M
        {
            get { return m; }
            set { m = value <= 0 ? 0 : value; }
        }
        public int Y
        {
            get { return y; }
            set { y = value <= 0 ? 0 : value; }
        }
        public int K
        {
            get { return k; }
            set { k = value <= 0 ? 0 : value; }
        }
        public PlayerHealth(int c, int m, int y, int k)
        {
            C = c;
            M = m;
            Y = y;
            K = k;
        }
        public void UpdateHealth(CMYK.PrimaryColor healthType, int delta)
        {
            switch (healthType)
            {
                case CMYK.PrimaryColor.Cyan:
                    C += delta;
                    break;
                case CMYK.PrimaryColor.Magenta:
                    M += delta;
                    break;
                case CMYK.PrimaryColor.Yellow:
                    Y += delta;
                    break;
                case CMYK.PrimaryColor.Black:
                    K += delta;
                    break;
            }
        }
        public void DamageAll(int damage)
        {
            C -= damage;
            M -= damage;
            Y -= damage;
            K -= damage * 3;
        }
        public bool IsDefeated() { return C <= 0 && M <= 0 && Y <= 0 && K <= 0; }
    }
    public PlayerHealth health;
    private HealthBar healthBar;
    public int energy = 1;
    public int maxHandSize = 7;
    public event Action<int, PlayerHealth> OnHealthChanged;
    public Player()
    {
        health = new PlayerHealth(7, 2, 1, 0);
    }
    public void TakeDamage(int damage)
    {
        health.DamageAll(damage);
        OnDamageTaken(damage, health.C + health.M + health.Y + health.K);
    }
    public bool IsDefeated()
    {
        return health.IsDefeated();
    }
    public void OnDamageTaken(int damage, int currentHealth)
    {
        // Debug.Log($"Player took {damage} damage!");
        OnHealthChanged?.Invoke(damage, health);
    }
}
