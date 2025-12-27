using UnityEngine;
using GorohsExtensions.CMYKColorSpace.UnityEngine;

public abstract class Cards
{
    private CMYK.PrimaryColor colorType;
    public abstract void OnPlay();
}
