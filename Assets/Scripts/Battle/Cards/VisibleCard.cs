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
    [SerializeField] private SpriteRenderer backgroundRenderer;
    [SerializeField] private Canvas canvas;

    // カードの選択を可能にするコンポーネント
    [SerializeField] private SelectableObject selectable;

    // 背景の画像
    [SerializeField] private Sprite actionSprite;
    [SerializeField] private Sprite reactionSprite;
    [SerializeField] private Sprite mechanismSprite;
    [SerializeField] private Sprite specialBulletSprite;
    [SerializeField] private Sprite actionOverclockSprite;
    [SerializeField] private Sprite reactionOverclockSprite;
    [SerializeField] private Sprite mechanismOverclockSprite;
    [SerializeField] private Sprite specialBulletOverclockSprite;

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
        if (attachedCard.cardType == CardData.CardType.Action) 
        { 
            typeText = "行動";
            if (attachedCard.isOverclock)
            {
                backgroundRenderer.sprite = actionOverclockSprite;
            }
            else
            {
                backgroundRenderer.sprite = actionSprite;
            }
        }
        else if (attachedCard.cardType == CardData.CardType.Reaction) 
        { 
            typeText = "対応";
            if (attachedCard.isOverclock)
            {
                backgroundRenderer.sprite = reactionOverclockSprite;
            }
            else
            {
                backgroundRenderer.sprite = reactionSprite;
            }
        }
        else if (attachedCard.cardType == CardData.CardType.Mechanism)
        {
            typeText = "機能";
            if (attachedCard.isOverclock)
            {
                backgroundRenderer.sprite = mechanismOverclockSprite;
            }
            else
            {
                backgroundRenderer.sprite = mechanismSprite;
            }
        }
        else if (attachedCard.cardType == CardData.CardType.SpecialBullet)
        {
            typeText = "銃弾";
            if (attachedCard.isOverclock)
            {
                backgroundRenderer.sprite = specialBulletOverclockSprite;
            }
            else
            {
                backgroundRenderer.sprite = specialBulletSprite;
            }
        }
        typeInputArea.SetText(typeText);
        gunSpriteRenderer.sprite = attachedCard.gunSprite;
        selectable.mask.sprite = backgroundRenderer.sprite;
    }

}
