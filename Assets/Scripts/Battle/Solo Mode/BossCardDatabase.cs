using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BossCardDatabase : ScriptableObject
{
    [SerializeField] private BossData bossAttached;
    [SerializeField] private Sprite cardBackground;
    [SerializeField] private List<BossCardData> cardDataLists = new List<BossCardData>();

    public List<BossCardData> GetCardDataLists() { return cardDataLists; }
    public Sprite GetCardBackground() {  return cardBackground; }
}
