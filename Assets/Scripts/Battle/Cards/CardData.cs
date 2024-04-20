using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "Card", menuName ="CreateCard")]
public class CardData : ScriptableObject
{
    public enum CardType
    {
        Action,
        Reaction,
        Mechanism,
        SpecialBullet,
        Other
    }
    public enum CardPool
    {
        GunAndGun,
        OverHeat,
        WShout,
        UltraBommy
    }

    // カードの各種属性。上から順に カードプール/ID/名前/効果テキスト/コスト/種別/デッキに存在する枚数 を指す。
    // なお、カードのIDはカードプールにrelativeなので注意（つまり、GunAndGunとOverHeatそれぞれにカードID「01」がいる）
    
    [SerializeField] private CardPool cardPool = CardPool.UltraBommy;
    
    [SerializeField] private string cardId;
    
    [SerializeField] private string cardName;

    [TextArea(3, 10)]
    [SerializeField] private string cardEffectText;
    
    [SerializeField] private int cardCost;

    [SerializeField] private int additionalCost;

    [SerializeField] private CardType cardType;
    
    [SerializeField] private int numOfCards;

    [SerializeField] private bool isOverclock;

    [SerializeField] private GunsData attachedGunData;

    public EffectHub effectHub;

    public CardPool GetCardPool() { return cardPool; }
    public string GetCardId() {  return cardId; }
    public string GetCardName() {  return cardName; }
    public string GetCardEffectText() {  return cardEffectText.Replace("\\n","\n"); }
    public int GetCardCost() { return cardCost; }
    public int GetAdditionalCost() {  return additionalCost; }
    public CardType GetCardType() {  return cardType; }
    public int GetNumberOfCards() {  return numOfCards; }
    public bool IsOverclock() {  return isOverclock; }
    public GunsData GetAttachedGunData() {  return attachedGunData; }
    public EffectHub GetEffectHub() { return effectHub; }
}
