using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCard : Card
{
    // ���̃��^�J�[�h�ɕR�Â��Ă��鎋�F�\�ȃJ�[�h
    public BossVisibleCard bossVisibleCard;

    // ���̃��^�J�[�h�ɕR�Â��Ă���EffectHub�A�܂���ʌQ
    public EffectHub[] cardEffectHubs;

    // �f�[�^�󂯓n���p�̃v���p�e�B
    public string cardNameENG;
    public string[] effectTexts;
}
