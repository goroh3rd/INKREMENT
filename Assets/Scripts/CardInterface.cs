using GorohsExtensions.CMYKColorSpace.UnityEngine;
using UnityEngine;
using UnityEngine.UI;

public class CardInterface : MonoBehaviour
{
    [SerializeField] private Cards card;
    [SerializeField] private Image cardImage;
    public Cards Card => card;
    public void Init(Cards card)
    {
        this.card = card;
        cardImage.color = card.ColorType.ToCMYK().ToRGB();
        card.Init(this);
    }
    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
    public void Hide()
    {
        cardImage.enabled = false;
    }
    public void Show()
    {
        cardImage.enabled = true;
    }
    public void Play()
    {
        card.OnPlay();
    }
}
