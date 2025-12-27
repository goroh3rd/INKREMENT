using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyUIElement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI enemyNameText;
    [SerializeField] private Image healthBarFill;
    private Enemy enemy;
    public void Init(Enemy enemy)
    {
        this.enemy = enemy;
        enemyNameText.text = enemy.CurrentStats.enemyName;
        UpdateHealthBar();
    }
    private void UpdateHealthBar()
    {
        float fillAmount = (float)enemy.CurrentStats.maxHealth / enemy.InitialStats.maxHealth;
        healthBarFill.fillAmount = fillAmount;
    }
}
