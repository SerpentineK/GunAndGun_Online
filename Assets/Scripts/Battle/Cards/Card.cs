using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// カードの名前や効果、位置の情報を収納するオブジェクトのクラス。
// 生成時にVisibleCardとの紐づけを必要とする。
public class Card : Object
{
    public Card(VisibleCard vis) { visibleCard = vis; }

    // このメタカードに紐づいている視認可能なカード
    public VisibleCard visibleCard;

    // このメタカードに紐づいているEffectHub、つまり効果群
    public EffectHub cardEffectHub;

    // カードの完全なID（1つのデッキに同じカードが数枚存在するため、登録は生成時に行う）
    public string cardAbsId;

    // 手札にこのカードが存在するか否か。
    public bool in_Hand = false;

    // 現在どこにあるかを示すプロパティ
    public Field currentField;

    public bool isOverclock = false;

    // データ受け渡し用のプロパティ
    public string cardName;
    public string effectText;
    public int cost;
    public Sprite gunSprite;
    public CardData.CardType cardType;

    // 使用可能か否かを示すプロパティ
    public bool playable = true;

    
    // in_Handプロパティを操作するメソッド。
    public void ToggleInHand()
    {
        in_Hand = !in_Hand; 
    }

    public virtual void Play() { }
    public virtual void Reload() { }
}
