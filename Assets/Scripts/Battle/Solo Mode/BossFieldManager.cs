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

    // �@�e�f�b�L�Ƃ͕ʂɁA�{�X�ɂ̓{�X�f�b�L������̂ł���𐶐����Ȃ���΂Ȃ�Ȃ��B
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
        // �f�[�^���������肵�A�ꎞ�I�ȕϐ��Ɋi�[����
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
