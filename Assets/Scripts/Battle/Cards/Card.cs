using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// カードの名前や効果、位置の情報を収納するクラス。
public class Card : MonoBehaviour
{

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

    // Field間の移動に際し、以前までどこにいたのかを記録するプロパティ
    public Field previousField;

    public bool isOverclock = false;

    // データ受け渡し用のプロパティ
    public string cardName;
    public string effectText;
    public int cost;
    public int additionalCost;
    public Gun attachedGun;
    public Sprite gunSprite;
    public CardData.CardType cardType;

    // 使用可能か否かを示すプロパティ
    public bool playable = true;

    // 手札のField(コストの確認に必要)
    public HandField handField;

    public enum CardStatus
    {
        NULL,
        PLAYABLE,
        DISABLED_BY_EFFECT,
        NOT_ENOUGH_COST,
        NOT_ENOUGH_ADDITIONAL_COST,
        NAGUN_UNAVAILABLE
    }

    // in_Handプロパティを操作するメソッド。
    public void ToggleInHand()
    {
        in_Hand = !in_Hand;
    }

    public CardStatus ExamineBeforePlay()
    {
        CardStatus status = CardStatus.NULL;
        bool playableToken;
        bool costToken;
        bool additionalCostToken;
        bool overclockToken;

        // まず使用を(フリーズバレットやノイズジェネレータなどの効果によって)禁止されていないか確認する。
        if (this.playable)
        {
            playableToken = true;
        }
        // 何らかの効果によって使用できない。
        else
        {
            playableToken = false;
            status = CardStatus.DISABLED_BY_EFFECT;
        }

        // 次にコストが足りるか確認する。
        // コストが0の場合はタダで使えるので、その場合を除外する。
        if (cost == 0)
        {
            costToken = true;
        }
        // このカードが手札にあって手札枚数がコストより多い場合は使ってよい(等しい場合はダメ)。
        else if (in_Hand && cost < handField.cardCount)
        {
            costToken = true;
        }
        // このカードが手札になく、手札枚数がコスト以上の場合も使ってよい(このカードが手札枚数に含まれていないため)。
        else if (!in_Hand && cost <= handField.cardCount)
        {
            costToken = true;
        }
        // コストに支払う手札が足りない。
        else
        {
            costToken = false;
            status = CardStatus.NOT_ENOUGH_COST;
        }

        // 次に追加コストの有無とそれを支払えるかを確認する。
        // 追加コストが0の場合はここは素通りしてよい。
        if (additionalCost == 0)
        {
            additionalCostToken = true;
        }
        else
        {
            // このカードの親機銃の装填数を数える(マガジンに入っているカードの枚数ではないことに注意！　おのれダブルバレット)。
            int bulletsNum = this.attachedGun.currentBullets;
            // 装填数が追加コスト以上なら合格。
            if (additionalCost <= bulletsNum)
            {
                additionalCostToken = true;
            }
            // 追加コストを支払えない。
            else
            {
                additionalCostToken = false;
                status = CardStatus.NOT_ENOUGH_ADDITIONAL_COST;
            }
        }

        // オーバークロックは裏向きの機銃には使えないため、それも確認する。
        // オーバークロックでないなら素通り。
        if (!isOverclock)
        {
            overclockToken = true;
        }
        // オーバークロックでも機銃が表向きなら通ってよし。
        else if (!attachedGun.isOverclocked)
        {
            overclockToken = false;
        }
        // 裏向き機銃のオーバークロックなので使用不可。
        else
        {
            overclockToken = false;
            status = CardStatus.NAGUN_UNAVAILABLE;
        }

        // 全てのトークンが揃っておりプレイしてよいならtrueを、そうでないならfalseを返す。
        if (playableToken && costToken && additionalCostToken && overclockToken)
        {
            status = CardStatus.PLAYABLE;
        }

        return status;
    }

    public void Reload() { }
}
