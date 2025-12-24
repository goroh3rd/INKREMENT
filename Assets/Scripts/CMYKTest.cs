using UnityEngine;

public class CMYKTest : MonoBehaviour
{
    [SerializeField] private CMYK cmykColor;
    [SerializeField] private SpriteRenderer spriteRenderer;
    void Start()
    {
        cmykColor = CMYK.Magenta + CMYK.Yellow;
        UpdateColor(cmykColor);
    }
    private void OnValidate()
    {
        UpdateColor(cmykColor);
    }
    private void UpdateColor(CMYK newColor)
    {
        cmykColor = newColor;
        if (spriteRenderer != null)
        {
            spriteRenderer.color = cmykColor.ToRGB();
        }
    }
}
