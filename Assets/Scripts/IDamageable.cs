using UnityEngine;

public interface IDamageable
{
    void TakeDamage(int damage);
    bool IsDefeated();
    void OnDamageTaken(int damage, int currentHealth);
}
