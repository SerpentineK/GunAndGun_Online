using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// �J�[�h�̖��O����ʁA�ʒu�̏������[����N���X�B
public class Card : MonoBehaviour
{

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

    // Field�Ԃ̈ړ��ɍۂ��A�ȑO�܂łǂ��ɂ����̂����L�^����v���p�e�B
    public Field previousField;

    public bool isOverclock = false;

    // �f�[�^�󂯓n���p�̃v���p�e�B
    public string cardName;
    public string effectText;
    public int cost;
    public int additionalCost;
    public Gun attachedGun;
    public Sprite gunSprite;
    public CardData.CardType cardType;

    // �g�p�\���ۂ��������v���p�e�B
    public bool playable = true;

    // ��D��Field(�R�X�g�̊m�F�ɕK�v)
    public HandField handField;

    public enum CardStatus
    {
        NULL,
        PLAYABLE,
        DISABLED_BY_EFFECT,
        NOT_ENOUGH_COST,
        NOT_ENOUGH_ADDITIONAL_COST,
        NAGUN_UNAVAILABLE
    }

    // in_Hand�v���p�e�B�𑀍삷�郁�\�b�h�B
    public void ToggleInHand()
    {
        in_Hand = !in_Hand;
    }

    public CardStatus ExamineBeforePlay()
    {
        CardStatus status = CardStatus.NULL;
        bool playableToken;
        bool costToken;
        bool additionalCostToken;
        bool overclockToken;

        // �܂��g�p��(�t���[�Y�o���b�g��m�C�Y�W�F�l���[�^�Ȃǂ̌��ʂɂ����)�֎~����Ă��Ȃ����m�F����B
        if (this.playable)
        {
            playableToken = true;
        }
        // ���炩�̌��ʂɂ���Ďg�p�ł��Ȃ��B
        else
        {
            playableToken = false;
            status = CardStatus.DISABLED_BY_EFFECT;
        }

        // ���ɃR�X�g������邩�m�F����B
        // �R�X�g��0�̏ꍇ�̓^�_�Ŏg����̂ŁA���̏ꍇ�����O����B
        if (cost == 0)
        {
            costToken = true;
        }
        // ���̃J�[�h����D�ɂ����Ď�D�������R�X�g��葽���ꍇ�͎g���Ă悢(�������ꍇ�̓_��)�B
        else if (in_Hand && cost < handField.cardCount)
        {
            costToken = true;
        }
        // ���̃J�[�h����D�ɂȂ��A��D�������R�X�g�ȏ�̏ꍇ���g���Ă悢(���̃J�[�h����D�����Ɋ܂܂�Ă��Ȃ�����)�B
        else if (!in_Hand && cost <= handField.cardCount)
        {
            costToken = true;
        }
        // �R�X�g�Ɏx������D������Ȃ��B
        else
        {
            costToken = false;
            status = CardStatus.NOT_ENOUGH_COST;
        }

        // ���ɒǉ��R�X�g�̗L���Ƃ�����x�����邩���m�F����B
        // �ǉ��R�X�g��0�̏ꍇ�͂����͑f�ʂ肵�Ă悢�B
        if (additionalCost == 0)
        {
            additionalCostToken = true;
        }
        else
        {
            // ���̃J�[�h�̐e�@�e�̑��U���𐔂���(�}�K�W���ɓ����Ă���J�[�h�̖����ł͂Ȃ����Ƃɒ��ӁI�@���̂�_�u���o���b�g)�B
            int bulletsNum = this.attachedGun.currentBullets;
            // ���U�����ǉ��R�X�g�ȏ�Ȃ獇�i�B
            if (additionalCost <= bulletsNum)
            {
                additionalCostToken = true;
            }
            // �ǉ��R�X�g���x�����Ȃ��B
            else
            {
                additionalCostToken = false;
                status = CardStatus.NOT_ENOUGH_ADDITIONAL_COST;
            }
        }

        // �I�[�o�[�N���b�N�͗������̋@�e�ɂ͎g���Ȃ����߁A������m�F����B
        // �I�[�o�[�N���b�N�łȂ��Ȃ�f�ʂ�B
        if (!isOverclock)
        {
            overclockToken = true;
        }
        // �I�[�o�[�N���b�N�ł��@�e���\�����Ȃ�ʂ��Ă悵�B
        else if (!attachedGun.isOverclocked)
        {
            overclockToken = false;
        }
        // �������@�e�̃I�[�o�[�N���b�N�Ȃ̂Ŏg�p�s�B
        else
        {
            overclockToken = false;
            status = CardStatus.NAGUN_UNAVAILABLE;
        }

        // �S�Ẵg�[�N���������Ă���v���C���Ă悢�Ȃ�true���A�����łȂ��Ȃ�false��Ԃ��B
        if (playableToken && costToken && additionalCostToken && overclockToken)
        {
            status = CardStatus.PLAYABLE;
        }

        return status;
    }

    public void Reload() { }
}
