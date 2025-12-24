using UnityEngine;

public class CMYKTest : MonoBehaviour
{
    [SerializeField] private CMYK cmykColor;
    [SerializeField] private SpriteRenderer spriteRenderer;
    void Start()
    {
        Color rgbColor = cmykColor.ToRGB();
        Debug.Log($"CMYK({cmykColor.c}, {cmykColor.m}, {cmykColor.y}, {cmykColor.k}) to RGB: {rgbColor}");

        CMYK convertedBack = CMYK.FromRGB(rgbColor);
        Debug.Log($"RGB({rgbColor.r}, {rgbColor.g}, {rgbColor.b}) back to CMYK: ({convertedBack.c}, {convertedBack.m}, {convertedBack.y}, {convertedBack.k})");
    }
    private void OnValidate()
    {
        cmykColor.c = Mathf.Clamp01(cmykColor.c);
        cmykColor.m = Mathf.Clamp01(cmykColor.m);
        cmykColor.y = Mathf.Clamp01(cmykColor.y);
        cmykColor.k = Mathf.Clamp01(cmykColor.k);
        if (spriteRenderer != null)
        {
            spriteRenderer.color = cmykColor.ToRGB();
        }
    }
}
