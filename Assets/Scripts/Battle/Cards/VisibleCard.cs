using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Cardをメタデータを扱うクラスとし、自分の手札とセットにあるカードのみこのVisibleCardで常時表示したい。
// 生成時に新たなCardオブジェクトを生成し、自身に紐づける。
// プレハブと紐づけるのはこちら。
public class VisibleCard : MonoBehaviour
{
    // このVisibleCardが紐づいているCardオブジェクト
    public Card attachedCard;

    // データ入力用の領域設定
    [SerializeField] private TMP_Text nameInputArea;
    [SerializeField] private TMP_Text effectInputArea;
    [SerializeField] private TMP_Text costInputArea;
    [SerializeField] private TMP_Text typeInputArea;
    [SerializeField] private SpriteRenderer gunSpriteRenderer;
    [SerializeField] private SpriteRenderer coloredPanelRenderer;
    [SerializeField] private SpriteRenderer whitePanelRenderer;
    [SerializeField] private Canvas canvas;

    public void InitiateMetaCard() 
    {
        attachedCard = this.gameObject.AddComponent<Card>();
        attachedCard.visibleCard = this;
    }

    // 生成されたカードObjectにプロパティを用いて入力された値を反映するメソッド
    public void InputCardData()
    {
        canvas.worldCamera = Camera.main;
        nameInputArea.SetText(attachedCard.cardName);
        effectInputArea.SetText(attachedCard.effectText);
        costInputArea.SetText("COST\n" + string.Format("{0:00}", attachedCard.cost));
        string typeText = null;
        Color color = new();
        if (attachedCard.cardType == CardData.CardType.Action) 
        { 
            typeText = "行動";
            color.a = 1f;
            color.r = 0f;
            color.g = 230f;
            color.b = 230f;
        }
        else if (attachedCard.cardType == CardData.CardType.Reaction) 
        { 
            typeText = "対応";
            color.a = 1f;
            color.r = 200f;
            color.g = 0f;
            color.b = 200f;
        }
        else if (attachedCard.cardType == CardData.CardType.Mechanism)
        {
            typeText = "機能";
            color.a= 1f;
            color.r = 0f;
            color.g = 200f;
            color.b = 0f;
        }
        else if (attachedCard.cardType == CardData.CardType.SpecialBullet)
        {
            typeText = "銃弾";
            color.a = 1f;
            color.r = 230f;
            color.g = 230f;
            color.b = 0f;
        }
        typeInputArea.SetText(typeText);
        coloredPanelRenderer.color = color;
        coloredPanelRenderer.sortingLayerName = "Cards";
        coloredPanelRenderer.sortingOrder = 2;
        if (attachedCard.cardEffectHub.isOverclock)
        {
            whitePanelRenderer.color = Color.black;
        }
        else
        {
            whitePanelRenderer.color = Color.white;
        }
        gunSpriteRenderer.sprite = attachedCard.gunSprite;
    }

}
