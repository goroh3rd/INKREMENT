using UnityEngine;
using GorohsExtensions.CMYKColorSpace.UnityEngine;

public abstract class Cards
{
    [SerializeField] protected CMYK.PrimaryColor colorType;
    public CMYK.PrimaryColor ColorType => colorType;
    protected CardInterface cardInterface;
    public void Init(CardInterface cardInterface)
    {
        this.cardInterface = cardInterface;
    }
    public abstract void OnPlay();
}
