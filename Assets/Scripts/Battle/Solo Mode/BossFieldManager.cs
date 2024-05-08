using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BossFieldManager : FieldManager
{
    public static BossFieldManager instance;

    public Field bossDeckField;
    public Field bossUsedField;

    [SerializeField] private GameObject bossCardPrefab;

    public void Awake()
    {
        instance = this;
    }

    // 機銃デッキとは別に、ボスにはボスデッキがあるのでそれを生成しなければならない。
    public void CreateBossDeck(BossCardDatabase bossCardDatabase)
    {
        List<BossCardData> deckData =  bossCardDatabase.GetCardDataLists();
        foreach (var bossCard in deckData) 
        {
            CreateBossCard(bossCard);
        }
        ShuffleDeck(bossDeckField);
    }

    public void CreateBossCard(BossCardData cardData)
    {
        // データから情報を入手し、一時的な変数に格納する
        string cardName = cardData.GetCardName();
        string cardNameENG = cardData.GetCardNameENG();
        string[] cardEffectTexts = cardData.GetCardEffectTexts();
        int numOfCards = cardData.GetNumOfCards();
        EffectHub[] effectHubs = cardData.attachedHubs;

        for(int i = 0; i < numOfCards; i++)
        {
            GameObject createdCard = Instantiate(bossCardPrefab, bossDeckField.transform);

            createdCard.SetActive(false);

            BossVisibleCard visibleCard = createdCard.GetComponent<BossVisibleCard>();
            BossCard metaCard = createdCard.GetComponent<BossCard>();

            metaCard.cardAbsId = "GD_" + cardData.GetCardId() + "_" + string.Format("{0:00}", i);
            metaCard.cardName = cardName;
            metaCard.cardNameENG = cardNameENG;
            metaCard.effectTexts = cardEffectTexts;
            metaCard.cardEffectHubs = effectHubs;

            createdCard.name = metaCard.cardAbsId;

            visibleCard.InputCardData();

            bossDeckField.RegisterCard(metaCard);
        }
    }

}
