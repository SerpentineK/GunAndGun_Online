using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// ���̃N���X�̓J�[�h�̗̈�Ԃ̈ړ����Ǌ�����B
// �̈��6��8�AVoltage��Discard�̓g�b�v�J�[�h�ƃJ�E���^�[�ADeck�̓J�E���^�[�݂̂̕\���B
public class FieldManager : MonoBehaviour
{
    // �v���C���[�̎�D
    public Field hand;

    // �v���C���[�̃Z�b�g���ꂽ�J�[�h
    public Field set;

    // �v���C���[�́y�@�\�z
    public Field mechanism;

    // �v���C���[�̎̂ĎD
    public Field discard;

    // �v���C���[�̃{���e�[�W
    public Field voltage;

    // �v���C���[�̍��E�̋@�e
    public Gun rightGun;
    public Gun leftGun;

    // �v���C���[�̍��E�̋@�e�f�b�L�I�u�W�F�N�g�i�J�E���^�[�Ƃ��j
    public Field rightDeck;
    public Field leftDeck;

    // �v���C���[�̍��E�̋@�e�ւ̑��U
    public Field rightMagazine;
    public Field leftMagazine;

    // �f�b�L�J�[�h�̃v���n�u
    [SerializeField] private GameObject mergedPrefab;

    // �f�b�L�J�[�h�v���n�u�̔z��
    private GameObject[] cardPrefabs;


    public Field GetFieldComponent(GameObject fieldObject)
    {
        Field result = fieldObject.GetComponent<Field>();
        return result;
    }

    /// <summary>
    /// �w�肳�ꂽ�f�b�L���V���b�t�����鏈���B
    /// </summary>
    /// <param name="deckObject">�w��f�b�L��GameObject</param>
    public void ShuffleDeck(Field deckField)
    {
        List<Card> list = new List<Card>(deckField.cardList);
        for (int i = list.Count - 1; i > 0; i--)
        {
            var j = Random.Range(0, i + 1); // �����_���ŗv�f�ԍ����P�I�ԁi�����_���v�f�j
            var temp = list[i]; // ��ԍŌ�̗v�f�����m�ہitemp�j�ɂ����
            list[i] = list[j]; // �����_���v�f����ԍŌ�ɂ����
            list[j] = temp; // ���m�ۂ��������_���v�f�ɏ㏑��
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
    /// �w�肳�ꂽ�J�[�h�����ݒn����ړI�n�ֈړ������鏈���B
    /// </summary>
    /// <param name="currentField">�J�[�h�̌��ݒn</param>
    /// <param name="destinationField">�J�[�h�̖ړI�n</param>
    /// <param name="card">�J�[�h�{��</param>
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
    /// �@�e�f�[�^���炻�̋@�e�ɋA������f�b�L�̃J�[�h�I�u�W�F�N�g��S�Đ�������B
    /// </summary>
    /// <param name="gunData">�@�e�f�[�^</param>
    public void CreateFullDeck(GunsData gunData)
    {
        List<CardData> cardDataList = gunData.GetDeckDatabase().GetCardDataLists();
        // ����AEffectHub���ɑΉ�����J�[�h�̃f�[�^�͑��݂��邪�J�[�h����EffectHub���������邱�Ƃ͂ł��Ȃ��B
        // �����ŁA��x���̋@�e��EffectHub�����ׂēǂݍ���ł��炻���Ή���������@���Ƃ�B
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

    // ���CreateCards���\�b�h�ɑg�ݍ��ނ��߂�1�ϐ����\�b�h�B
    // �����CardData����f�b�L�ɓ����Ă��閇���������J�[�h�𐶐�����B
    public void CreateCardForDeck(CardData cardData, EffectHub effectHub)
    {
        // CardData�I�u�W�F�N�g���������肷��B
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

        // �f�b�L�ɏd�����ē����Ă��閇�����J�[�h�𐶐�����B
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
