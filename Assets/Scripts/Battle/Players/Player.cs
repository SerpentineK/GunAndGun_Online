using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    // �v���C���[�ɋA������}�l�[�W���[
    public FieldManager FM;
    public EffectManager EM;
    public SkillManager SM;

    // StartScene�ɂđI�����ꂽ�f�[�^
    public GunnerData gunnerData;
    public GunsData rightGunsData;
    public GunsData leftGunsData;
    public SkillData skillData;

    // ���_�̃v���C���[���ۂ�
    public bool isProtagonist;

    // �^�[���v���C���[���ۂ�
    public bool isTurn;

    // �q�b�g�|�C���g
    public int HP = 30;
    public TMP_Text HP_Counter;

    // ��D����
    public int handNum;

    // ���̃v���C���[�̏e�m�A�@�e�A�Z�\�I�u�W�F�N�g
    public Gunner gunner;
    public Gun rightGun;
    public Gun leftGun;
    public Skill skill;

    public void InputPlayerData() 
    {
        gunner.data = gunnerData;
        gunner.InputGunnerData();
        rightGun.data = rightGunsData;
        rightGun.InputGunData();
        leftGun.data = leftGunsData;
        leftGun.InputGunData();
        skill.data = skillData;
        skill.InputSkillData();
        HP_Counter.SetText(string.Format("{0:00}", HP));
        handNum = gunner.hand;
    }
 
    
    public void DrawCardsAsRule()
    {
        int currentHandNum = FM.hand.cardCount;
        int numToDraw = handNum - currentHandNum;
        if (numToDraw > 0)
        {
            FM.DrawFromDeck(numToDraw,FM.leftDeck);
        }
    }
}
