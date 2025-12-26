using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private SpriteRenderer healthBarElement;
    [SerializeField] private SpriteRenderer heartIcon;
    private Player player;
    private List<SpriteRenderer> healthBarElements = new List<SpriteRenderer>();
    public void Init(Player p)
    {
        this.player = p;
        player.OnHealthChanged += UpdateHealthBar;
    }
    public void UpdateHealthBar(int damage, Player.PlayerHealth playerHealth)
    {
        // ヘルスバーの更新ロジックをここに実装
        Reset();
        Vector3 settingPosition = Vector3.zero;
        for (int i = 0; i < playerHealth.K; i++)
        {
            SpriteRenderer element = Instantiate(healthBarElement, transform);
            element.color = CMYK.Black.ToRGB();
            element.transform.localPosition = settingPosition;
            healthBarElements.Add(element);
            settingPosition.y += element.bounds.size.y - 0.125f;
        }
        for (int i = 0; i < playerHealth.Y; i++)
        {
            SpriteRenderer element = Instantiate(healthBarElement, transform);
            element.color = (CMYK.Yellow + new CMYK(0, 0, 0, 0.1f)).ToRGB();
            element.transform.localPosition = settingPosition;
            healthBarElements.Add(element);
            settingPosition.y += element.bounds.size.y - 0.125f;
        }
        for (int i = 0; i < playerHealth.M; i++)
        {
            SpriteRenderer element = Instantiate(healthBarElement, transform);
            element.color = (CMYK.Magenta + new CMYK(0, 0, 0, 0.1f)).ToRGB();
            element.transform.localPosition = settingPosition;
            healthBarElements.Add(element);
            settingPosition.y += element.bounds.size.y - 0.125f;
        }
        for (int i = 0; i < playerHealth.C; i++)
        {
            SpriteRenderer element = Instantiate(healthBarElement, transform);
            element.color = (CMYK.Cyan + new CMYK(0, 0, 0, 0.1f)).ToRGB();
            element.transform.localPosition = settingPosition;
            healthBarElements.Add(element);
            settingPosition.y += element.bounds.size.y - 0.125f;
        }
        // 色の割合に応じて平均化
        heartIcon.color = CMYK.Average(healthBarElements.Select(e => CMYK.From(e.color)).ToList()).ToColor();
    }
    private void Reset()
    {
        healthBarElements.ForEach(element => Destroy(element.gameObject));
        healthBarElements.Clear();
    }
    private void OnDestroy()
    {
        Reset();
        // player.OnHealthChanged -= UpdateHealthBar;
    }
    [ContextMenu("TestUpdate")]
    public void TestUpdate()
    {
        Reset();
        UpdateHealthBar(0, new Player.PlayerHealth(Random.Range(0, 5), Random.Range(0, 5), Random.Range(0, 5), Random.Range(0, 5)));
    }
}