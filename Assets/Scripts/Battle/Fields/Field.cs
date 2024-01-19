using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using TMPro;
using Unity.VisualScripting;

// カードが存在し得る場所Fieldは
// Deck デッキ
// Hand 手札
// Set セットゾーン
// Discard 捨て札
// Voltage ボルテージ
// GunMagazine 銃の弾倉
// の6種類が存在する。これらをFieldクラスで実装する。

public class Field : MonoBehaviour
{
    public bool isPlayer;
    public List<Card> cardList = new();
    public int cardCount = 0;
    public TMP_Text cardCounterObj;

    public void UpdateCounter()
    {
        cardCount = cardList.Count;
        if (!cardCounterObj.IsUnityNull()) { cardCounterObj.SetText(string.Format("{0:00}", cardCount)); }
    }

    public virtual void RegisterCard(Card card)
    {
        cardList.Add(card);
        card.currentField = this;
        UpdateCounter();
    }

    public virtual void RemoveCard(Card card)
    {
        cardList.Remove(card);
        card.currentField = null;
        UpdateCounter();
    }

}
