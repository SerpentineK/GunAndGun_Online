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

    // �J�[�h�̊e�푮���B�ォ�珇�� �J�[�h�v�[��/ID/���O/���ʃe�L�X�g/�R�X�g/���/�f�b�L�ɑ��݂��閇�� ���w���B
    // �Ȃ��A�J�[�h��ID�̓J�[�h�v�[����relative�Ȃ̂Œ��Ӂi�܂�AGunAndGun��OverHeat���ꂼ��ɃJ�[�hID�u01�v������j
    
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
