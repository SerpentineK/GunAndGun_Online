using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossCardData : ScriptableObject
{
    [SerializeField] private string cardId;
    [SerializeField] private string cardName;
    [SerializeField] private string cardNameENG;
    [TextArea(2,10)]
    [SerializeField] private string[] cardEffectTexts;
    [SerializeField] private int numOfCards;
    public EffectHub[] attachedHubs;

    public string GetCardId() {  return cardId; }
    public string GetCardName() {  return cardName; }
    public string GetCardNameENG() {  return cardNameENG; }
    public string[] GetCardEffectTexts() {  return cardEffectTexts; }
    public int GetNumOfCards() { return numOfCards; }
}
