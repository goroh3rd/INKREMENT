using UnityEngine;
using UnityEngine.UI;
using GorohsExtensions.CMYKColorSpace.UnityEngine;

public abstract class Cards
{
    protected CMYK.PrimaryColor colorType;
    public CMYK.PrimaryColor ColorType => colorType;
    protected string cardName;
    public abstract string CardName { get; }
    protected string description;
    public abstract string Description { get; }
    protected CardInterface cardInterface;
    protected BattleManager battleManager;
    public BattleManager BattleManager => battleManager;
    public Cards(BattleManager battleManager, CardInterface cInterface)
    {
        this.battleManager = battleManager;
        this.cardInterface = cInterface;
        cInterface.Init(this);
    }
    public abstract void OnDraw();
    public abstract void OnDiscard();
    public abstract void OnPlay();
}
