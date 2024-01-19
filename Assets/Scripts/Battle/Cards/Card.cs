using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// �J�[�h�̖��O����ʁA�ʒu�̏������[����I�u�W�F�N�g�̃N���X�B
// ��������VisibleCard�Ƃ̕R�Â���K�v�Ƃ���B
public class Card : Object
{
    public Card(VisibleCard vis) { visibleCard = vis; }

    // ���̃��^�J�[�h�ɕR�Â��Ă��鎋�F�\�ȃJ�[�h
    public VisibleCard visibleCard;

    // ���̃��^�J�[�h�ɕR�Â��Ă���EffectHub�A�܂���ʌQ
    public EffectHub cardEffectHub;

    // �J�[�h�̊��S��ID�i1�̃f�b�L�ɓ����J�[�h���������݂��邽�߁A�o�^�͐������ɍs���j
    public string cardAbsId;

    // ��D�ɂ��̃J�[�h�����݂��邩�ۂ��B
    public bool in_Hand = false;

    // ���݂ǂ��ɂ��邩�������v���p�e�B
    public Field currentField;

    public bool isOverclock = false;

    // �f�[�^�󂯓n���p�̃v���p�e�B
    public string cardName;
    public string effectText;
    public int cost;
    public Sprite gunSprite;
    public CardData.CardType cardType;

    // �g�p�\���ۂ��������v���p�e�B
    public bool playable = true;

    
    // in_Hand�v���p�e�B�𑀍삷�郁�\�b�h�B
    public void ToggleInHand()
    {
        in_Hand = !in_Hand; 
    }

    public virtual void Play() { }
    public virtual void Reload() { }
}
