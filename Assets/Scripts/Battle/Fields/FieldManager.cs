using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// このクラスはカードの領域間の移動を管轄する。
// 領域は6種8個、VoltageとDiscardはトップカードとカウンター、Deckはカウンターのみの表示。
public class FieldManager : MonoBehaviour
{
    // プレイヤーの手札
    public Field hand;

    // プレイヤーのセットされたカード
    public Field set;

    // プレイヤーの【機能】
    public Field mechanism;

    // プレイヤーの捨て札
    public Field discard;

    // プレイヤーのボルテージ
    public Field voltage;

    // プレイヤーの左右の機銃
    public Gun rightGun;
    public Gun leftGun;

    // プレイヤーの左右の機銃デッキオブジェクト（カウンターとか）
    public Field rightDeck;
    public Field leftDeck;

    // プレイヤーの左右の機銃への装填
    public Field rightMagazine;
    public Field leftMagazine;

    // デッキカードのプレハブ
    [SerializeField] private GameObject mergedPrefab;

    // デッキカードプレハブの配列
    private GameObject[] cardPrefabs;


    public Field GetFieldComponent(GameObject fieldObject)
    {
        Field result = fieldObject.GetComponent<Field>();
        return result;
    }

    /// <summary>
    /// 指定されたデッキをシャッフルする処理。
    /// </summary>
    /// <param name="deckObject">指定デッキのGameObject</param>
    public void ShuffleDeck(Field deckField)
    {
        List<Card> list = new List<Card>(deckField.cardList);
        for (int i = list.Count - 1; i > 0; i--)
        {
            var j = Random.Range(0, i + 1); // ランダムで要素番号を１つ選ぶ（ランダム要素）
            var temp = list[i]; // 一番最後の要素を仮確保（temp）にいれる
            list[i] = list[j]; // ランダム要素を一番最後にいれる
            list[j] = temp; // 仮確保を元ランダム要素に上書き
        }
        deckField.cardList = list;
    }

    public Card[] ChooseCards(int number,  Field[] fields)
    {
        Card[] chosenArray = new Card[number];
        if (fields.Length == 1) 
        {
            if (fields[0] == hand)
            {
                
            }
        }
        List<Card> tempList = new List<Card>();
        for (int i = 0; i < fields.Length; i++) 
        {
            fields[i].cardList.ForEach(tempList.Add);
        }
        
        return chosenArray;
    }

    /// <summary>
    /// 指定されたカードを現在地から目的地へ移動させる処理。
    /// </summary>
    /// <param name="currentField">カードの現在地</param>
    /// <param name="destinationField">カードの目的地</param>
    /// <param name="card">カード本体</param>
    /// <returns></returns>
    public void TransferCard(Field currentField, Field destinationField, Card card)
    {
        currentField.RemoveCard(card);
        destinationField.RegisterCard(card);
    }


    public void DrawFromDeck(int number, Field deck) 
    {
        for (int i = 0; i < number; i++) 
        {
            Card targetCard = deck.cardList[0];
            TransferCard(deck, hand, targetCard);
        }
    }

    /// <summary>
    /// 機銃データからその機銃に帰属するデッキのカードオブジェクトを全て生成する。
    /// </summary>
    /// <param name="gunData">機銃データ</param>
    public void CreateFullDeck(GunsData gunData)
    {
        List<CardData> cardDataList = gunData.GetDeckDatabase().GetCardDataLists();
        // 現状、EffectHub側に対応するカードのデータは存在するがカードからEffectHubを検索することはできない。
        // そこで、一度この機銃のEffectHubをすべて読み込んでからそれを対応させる方法をとる。
        List<EffectHub> hubDataList = gunData.GetDeckDatabase().GetEffectHubDatabase().GetEffectHubList();
        // cardDataList.ForEach(CreateCardForDeck);
        foreach (var cardData in cardDataList)
        {
            EffectHub effectHub = null;
            foreach (var hubCandidate in hubDataList) 
            {
                if (hubCandidate.attachedData == cardData) 
                { 
                    effectHub = hubCandidate;
                }
            }
            CreateCardForDeck(cardData, effectHub);
        }
    }

    // 上のCreateCardsメソッドに組み込むための1変数メソッド。
    // 特定のCardDataからデッキに入っている枚数分だけカードを生成する。
    public void CreateCardForDeck(CardData cardData, EffectHub effectHub)
    {
        // CardDataオブジェクトから情報を入手する。
        string cardName = cardData.GetCardName();
        string cardEffectText = cardData.GetCardEffectText();
        int cardCost = cardData.GetCardCost();
        CardData.CardType cardType = cardData.GetCardType();
        int numberOfCards = cardData.GetNumberOfCards();
        GunsData attachedGunData = cardData.GetAttachedGunData();
        Sprite gunImage = attachedGunData.GetGunImage();

        Field parentDeck = leftDeck.GetComponent<DeckField>();
        if (cardData.GetAttachedGunData() == rightGun.data)
        {
            parentDeck = rightDeck.GetComponent<DeckField>();
        }

        // デッキに重複して入っている枚数分カードを生成する。
        for (int i = 1; i <= numberOfCards; i++)
        {
            GameObject createdCard = Instantiate(mergedPrefab, parentDeck.transform);

            createdCard.SetActive(false);

            VisibleCard visibleCard = createdCard.GetComponent<VisibleCard>().InitiateMetaCard();
            Card metaCard = visibleCard.attachedCard;
            
            metaCard.cardAbsId= "GD_" + cardData.GetCardId() + "_" + string.Format("{0:00}", i);
            metaCard.cardName = cardName;
            metaCard.effectText = cardEffectText;
            metaCard.cost = cardCost;
            metaCard.gunSprite = gunImage;
            metaCard.cardType = cardType;
            metaCard.cardEffectHub = effectHub;
            metaCard.isOverclock = effectHub.isOverclock;

            createdCard.name = metaCard.cardAbsId;

            visibleCard.InputCardData();

            parentDeck.RegisterCard(metaCard);
        }
    }

}
