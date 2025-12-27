using UnityEngine;
using UnityEngine.UI;
using GorohsExtensions.CMYKColorSpace.UnityEngine;

public class Resist : Cards
{
    new CMYK.PrimaryColor colorType = CMYK.PrimaryColor.Cyan;
    new string cardName = "抵抗";
    public override string CardName => cardName;
    new string description = "敵に3のダメージを与える。";
    public override string Description => description;
    public Resist(BattleManager battleManager, CardInterface cardInterface) : base(battleManager, cardInterface)
    {
    }
    public override void OnDraw()
    {
        // カードが引かれたときの処理をここに実装
        cardInterface.Appear();
    }
    public override void OnDiscard()
    {
        // カードが捨てられたときの処理をここに実装
        cardInterface.Disappear();
    }
    public override void OnPlay()
    {
        // カードがプレイされたときの処理をここに実装
        battleManager.enemies[0].TakeDamage(3);
        battleManager.Player.Heal(CMYK.PrimaryColor.Cyan, 1);
        Debug.Log("Resist played");
        OnDiscard();
    }
}
