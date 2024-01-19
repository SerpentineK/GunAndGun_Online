using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardDataBase", menuName = "CreateCardDataBase")]
public class CardDataBase : ScriptableObject
{
    [SerializeField] private GunsData gunAttached;
    [SerializeField] private List<CardData> cardDataLists = new List<CardData>();
    [SerializeField] private EffectHubDatabase effectHubDatabaseAttached;

    public List<CardData> GetCardDataLists() { return cardDataLists; }
    public EffectHubDatabase GetEffectHubDatabase() {  return effectHubDatabaseAttached; }
}
