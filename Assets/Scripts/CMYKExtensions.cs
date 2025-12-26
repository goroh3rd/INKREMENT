using System.Collections.Generic;
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
    public static Color CMYKtoRGB(CMYK cmyk)
    {
        return CMYKtoRGB(cmyk.c, cmyk.m, cmyk.y, cmyk.k);
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
    public static Color SubtractiveLerp(Color a, Color b, float t) => CMYK.Lerp(CMYK.From(a), CMYK.From(b), t).ToColor();
    public static Color SubtractiveBlend(this Color a, Color b)
    {
        CMYK ca = CMYK.From(a);
        CMYK cb = CMYK.From(b);
        return (ca + cb).ToColor();
    }
    public static void SetCMYK(this Material mat, string property, CMYK cmyk) => mat.SetColor(property, cmyk.ToColor());
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

    public Color ToRGB() => CMYKExtensions.CMYKtoRGB(this);
    public Color ToColor() => ToRGB();
    public static CMYK From(Color color) => FromRGB(color);
    public static CMYK ToCMYK(Color color) => FromRGB(color);

    /// <summary> Convert from RGB Color to CMYK struct </summary>
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
    public static CMYK operator *(CMYK a, float scalar)
    {
        return new CMYK(
            a.c * scalar,
            a.m * scalar,
            a.y * scalar,
            a.k * scalar
        ).Clamped();
    }
    public static bool operator ==(CMYK a, CMYK b) => a.Equals(b);
    public static bool operator !=(CMYK a, CMYK b) => !(a == b);

    public CMYK Clamped()
    {
        return new CMYK(
            Mathf.Clamp01(c),
            Mathf.Clamp01(m),
            Mathf.Clamp01(y),
            Mathf.Clamp01(k)
        );
    }
    public override bool Equals(object obj)
    {
        if (!(obj is CMYK cMYK)) return false;
        return Mathf.Approximately(c, cMYK.c)
            && Mathf.Approximately(m, cMYK.m)
            && Mathf.Approximately(y, cMYK.y)
            && Mathf.Approximately(k, cMYK.k);
    }
    public override int GetHashCode() => (c, m, y, k).GetHashCode();
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
    public static CMYK Average(params CMYK[] colors)
    {
        return Average((IEnumerable<CMYK>)colors);
    }
    public static CMYK Average(IEnumerable<CMYK> colors)
    {
        var colorList = new List<CMYK>(colors);
        if (colorList.Count == 0) return Zero;
        float totalC = 0f, totalM = 0f, totalY = 0f, totalK = 0f;
        foreach (var color in colorList)
        {
            totalC += color.c;
            totalM += color.m;
            totalY += color.y;
            totalK += color.k;
        }
        return new CMYK(
            totalC / colorList.Count,
            totalM / colorList.Count,
            totalY / colorList.Count,
            totalK / colorList.Count
        );
    }
    public static CMYK Zero => new CMYK(0, 0, 0, 0);
    public static CMYK Black => new CMYK(0, 0, 0, 1);
    public static CMYK White => new CMYK(0, 0, 0, 0);
    public static CMYK Red => new CMYK(0, 1, 1, 0);
    public static CMYK Green => new CMYK(1, 0, 1, 0);
    public static CMYK Blue => new CMYK(1, 1, 0, 0);
    public static CMYK Yellow => new CMYK(0, 0, 1, 0);
    public static CMYK Cyan => new CMYK(1, 0, 0, 0);
    public static CMYK Magenta => new CMYK(0, 1, 0, 0);
    public enum PrimaryColor
    {
        Cyan,
        Magenta,
        Yellow,
        Black
    }
}
