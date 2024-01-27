using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySelectionData : NetworkBehaviour
{
    // SSM���i�[����ϐ��i���n��SSM���~�����̂�Networked�͂��Ȃ��j
    public StartSceneManager SSM;

    // ���[�J���v���C���[�ɂ��Ă̏�񂩔ۂ�bool�i�����Client�ɂ���ĕς�邽��Networked�͂��Ȃ��j
    public bool isAboutLocalPlayer;

    // �e��Data���i�[���邽�߂̃��b�p�[�N���X
    [Networked] public OnlineGunnerData MyGunner { get; set; }
    [Networked] public OnlineGunsData MyGun01 { get; set; }
    [Networked] public OnlineGunsData MyGun02 { get; set; }
    [Networked] public OnlineSkillData MySkill { get; set; }

    public override void Spawned()
    {
        // SSM���擾����
        SSM = FindAnyObjectByType<StartSceneManager>();

        // LocalPlayer��StateAuthority���A�܂肱��NetworkObject�ɂ��ď�Ԍ����������Ă��邩���m�F����
        if (Runner.LocalPlayer == Object.StateAuthority) { isAboutLocalPlayer = true; }
        else { isAboutLocalPlayer = false; }
    }

    public override void FixedUpdateNetwork()
    {
        // ����v���C���[�{�l�ɂ��Ă̏��̏ꍇ�͌��nSSM����MyWhatever�֓���
        if (isAboutLocalPlayer)
        {
            if (SSM.playerGunner != null && MyGunner.gunnerData == null)
            {
                MyGunner.gunnerData = SSM.playerGunner;
            }
            if (SSM.playerGun01 != null && MyGun01.gunsData == null)
            {
                MyGun01.gunsData = SSM.playerGun01;
            }
            if (SSM.playerGun02 != null && MyGun02.gunsData == null)
            {
                MyGun02.gunsData = SSM.playerGun02;
            }
            if (SSM.playerSkill != null && MySkill.skillData == null)
            {
                MySkill.skillData = SSM.playerSkill;
            }
        }
        // �ΐ푊��ɂ��Ă̏��̏ꍇ��MyWhatever���猻�nSSM�֓���
        else
        {
            if (SSM.opponentGunner == null && MyGunner.gunnerData != null)
            {
                SSM.opponentGunner = MyGunner.gunnerData;
            }
            if (SSM.opponentGun01 == null && MyGun01.gunsData != null)
            {
                MyGun01.gunsData = SSM.playerGun01;
            }
            if (SSM.SecondGunToken && MyGun02.gunsData == null)
            {
                MyGun02.gunsData = SSM.playerGun02;
            }
            if (SSM.SkillToken && MySkill.skillData == null)
            {
                MySkill.skillData = SSM.playerSkill;
            }
        }
    }
}
