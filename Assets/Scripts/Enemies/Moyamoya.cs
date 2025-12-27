using UnityEngine;

public class Moyamoya : Enemy
{
    protected override void UpdatePredictUI()
    {
        // Moyamoyaの予測UI更新ロジックをここに実装
        uiElement.UpdatePredictedAction(CurrentStats.attackPower.ToString() + "ダメージ\n" + "この敵はダメージを受けると攻撃力が下がる");
    }
    public override void OnAction()
    {
        // Moyamoyaの行動ロジックをここに実装
        battleManager.Player.TakeDamage(CurrentStats.attackPower);
    }
    public override void OnDamageTaken(int damage, int currentHealth)
    {
        // ダメージを受けたときの処理をここに実装
        UpdateStats(new EnemyStats(CurrentStats.enemyName, currentHealth, CurrentStats.attackPower - damage));
        UpdatePredictUI();
    }
}
