using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableReload : AbstractEffect_Disable
{
    public Gun[] disabledGuns;
    // �ʏ�e�̑��U�������邩(�h�h����z��)
    public bool allowNormalBullets;
    // ����e�̑��U�������邩(�_�C�_����z��)
    public bool allowSpecialBullets;
    // �ʏ�A�N�V�����ɂ�鑕�U�������邩�i�s���|�e�t��z��j
    public bool allowNormalReload;
    // ���ʂɂ�鑕�U�������邩(�s�t���[�Y�o���b�g�t��z��)
    public bool allowCardEffects;
}
