using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using TMPro;
using Unity.VisualScripting;

// �J�[�h�����݂�����ꏊField��
// Deck �f�b�L
// Hand ��D
// Set �Z�b�g�]�[��
// Discard �̂ĎD
// Voltage �{���e�[�W
// GunMagazine �e�̒e�q
// ��6��ނ����݂���B������Field�N���X�Ŏ�������B

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
