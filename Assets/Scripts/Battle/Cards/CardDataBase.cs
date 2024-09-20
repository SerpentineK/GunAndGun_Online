using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardDatabase : ScriptableObject
{
    [SerializeField] private GunsData gunAttached;
    [SerializeField] private List<CardData> cardDataLists = new List<CardData>();
    [SerializeField] private EffectHubDatabase effectHubDatabaseAttached;
    [SerializeField] private Sprite deckDetails;

    public List<CardData> GetCardDataLists() { return cardDataLists; }
    public EffectHubDatabase GetEffectHubDatabase() {  return effectHubDatabaseAttached; }

    public Sprite DeckDetails { get {  return deckDetails; } }
}
