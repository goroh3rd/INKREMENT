using GorohsExtensions.CMYKColorSpace.UnityEngine;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class CardInterface : MonoBehaviour
{
    [SerializeField] private Cards card;
    [SerializeField] private Image cardImage;
    [SerializeField] private TextMeshProUGUI cardNameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    private static Vector3 initialPos = new Vector3(-1, -3, 0);
    public Cards Card => card;
    public void Init(Cards card)
    {
        this.card = card;
        cardImage.color = card.ColorType.ToCMYK().ToRGB();
        cardNameText.text = card.CardName;
        descriptionText.text = card.Description;
        Hide();
    }
    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
    public void Appear()
    {
        Show();
        this.transform.position = initialPos + cardImage.sprite.bounds.size.x * cardImage.transform.localScale.x * Vector3.right * card.BattleManager.CardManager.hand.IndexOf(card);
        this.transform.DOLocalMoveY(this.transform.localPosition.y + 0.5f, 0.5f).SetEase(Ease.OutBack).SetDelay(0.1f * card.BattleManager.CardManager.hand.IndexOf(card));
    }
    public void Disappear()
    {
        this.transform.DOLocalMoveY(this.transform.localPosition.y - 3, 0.5f).SetEase(Ease.InCubic).OnComplete(() => {
            Hide();
        });
    }
    public void Hide()
    {
        cardImage.enabled = false;
        cardImage.raycastTarget = false;
        cardNameText.enabled = false;
        descriptionText.enabled = false;
        // this.transform.SetParent(parentRect.parent);
    }
    public void Show()
    {
        cardImage.enabled = true;
        cardImage.raycastTarget = true;
        cardNameText.enabled = true;
        descriptionText.enabled = true;
        // this.transform.SetParent(parentRect);
    }
    public void Play()
    {
        card.OnPlay();
    }
}
