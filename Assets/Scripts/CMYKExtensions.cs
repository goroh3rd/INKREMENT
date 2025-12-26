using TMPro;
using UnityEngine;

// This class is intended to hold extension methods related to CMYK color space.
public static class CMYKExtensions
{
    public static Color CMYKtoRGB(float c, float m, float y, float k)
    {
        float r = (1 - c) * (1 - k);
        float g = (1 - m) * (1 - k);
        float b = (1 - y) * (1 - k);
        return new Color(r, g, b, 1f);
    }
    public static Color CMYKtoRGB(Color cmyk)
    {
        return CMYKtoRGB(cmyk.r, cmyk.g, cmyk.b, cmyk.a);
    }
    public static Color RGBtoCMYK(this Color rgb)
    {
        float r = rgb.r;
        float g = rgb.g;
        float b = rgb.b;

        float k = 1 - Mathf.Max(r, Mathf.Max(g, b));
        float c = (1 - r - k) / (1 - k);
        float m = (1 - g - k) / (1 - k);
        float y = (1 - b - k) / (1 - k);

        if (float.IsNaN(c)) c = 0;
        if (float.IsNaN(m)) m = 0;
        if (float.IsNaN(y)) y = 0;

        return new Color(c, m, y, k);
    }

}
[System.Serializable]
public struct CMYK
{
    [SerializeField, Range(0.0f, 1f)]
    public float c;
    [SerializeField, Range(0.0f, 1f)]
    public float m;
    [SerializeField, Range(0.0f, 1f)]
    public float y;
    [SerializeField, Range(0.0f, 1f)]
    public float k;

    public CMYK(float c, float m, float y, float k)
    {
        c = Mathf.Clamp01(c);
        m = Mathf.Clamp01(m);
        y = Mathf.Clamp01(y);
        k = Mathf.Clamp01(k);
        this.c = c;
        this.m = m;
        this.y = y;
        this.k = k;
    }

    public Color ToRGB()
    {
        return CMYKExtensions.CMYKtoRGB(c, m, y, k);
    }

    public static CMYK FromRGB(Color rgb)
    {
        Color cmykColor = rgb.RGBtoCMYK();
        return new CMYK(cmykColor.r, cmykColor.g, cmykColor.b, cmykColor.a);
    }
    public static CMYK operator +(CMYK a, CMYK b)
    {
        return new CMYK(
            c: 1f - (1f - a.c) * (1f - b.c),
            m: 1f - (1f - a.m) * (1f - b.m),
            y: 1f - (1f - a.y) * (1f - b.y),
            k: 1f - (1f - a.k) * (1f - b.k)
        ).Clamped();
    }
    public CMYK Clamped()
    {
        return new CMYK(
            Mathf.Clamp01(c),
            Mathf.Clamp01(m),
            Mathf.Clamp01(y),
            Mathf.Clamp01(k)
        );
    }
    public static CMYK Lerp(CMYK a, CMYK b, float t)
    {
        t = Mathf.Clamp01(t);
        return new CMYK(
            Mathf.Lerp(a.c, b.c, t),
            Mathf.Lerp(a.m, b.m, t),
            Mathf.Lerp(a.y, b.y, t),
            Mathf.Lerp(a.k, b.k, t)
        );
    }

    public static CMYK Black => new CMYK(0, 0, 0, 1);
    public static CMYK White => new CMYK(0, 0, 0, 0);
    public static CMYK Red => new CMYK(0, 1, 1, 0);
    public static CMYK Green => new CMYK(1, 0, 1, 0);
    public static CMYK Blue => new CMYK(1, 1, 0, 0);
    public static CMYK Yellow => new CMYK(0, 0, 1, 0);
    public static CMYK Cyan => new CMYK(1, 0, 0, 0);
    public static CMYK Magenta => new CMYK(0, 1, 0, 0);
}
