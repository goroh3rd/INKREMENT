using UnityEngine;
using DG.Tweening;

public static class DOTweenCMYKExtensions
{
    public static Tweener DOColorCMYK(this SpriteRenderer target, CMYK targetColor, float duration)
    {
        float start = 0f;
        return DOTween.To(
            () => start,
            x =>
            {
                start = x;
                target.color = CMYK.Lerp(CMYK.From(target.color), targetColor, start).ToColor();
                Color rgb = target.color;
                rgb.a = target.color.a;
                target.color = rgb;
            },
            1f,
            duration
        );
    }
}
