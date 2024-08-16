using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

// ���̃N���X�̓v���C���[�̃��\�[�X���ׂĂ��Ǘ����鉼�z�Ֆʂ�\���B
// �v���C���[1�l�ɂ����̃N���X�̃C���X�^���X��1�ł���A�l�b�g���[�N��ł͋��L�I�u�W�F�N�g�Ƃ��Ĉ����B
public class VirtualGameboard : NetworkBehaviour
{

    // �v���C���[��
    [Networked] public NetworkString<_16> NickName { get; private set; }

    // �v���C���[�ɑΉ�����Player�I�u�W�F�N�g
    [Networked] public Player MyPlayer { get; private set; }

    // �e�̈�NetworkedField��Networked�v���p�e�B�B
    [HideInInspector][Networked] public NetworkedField RightDeckCards { get; private set; }
    [HideInInspector][Networked] public NetworkedField LeftDeckCards { get; private set; }
    [HideInInspector][Networked] public NetworkedField RightMagazineCards { get; private set; }
    [HideInInspector][Networked] public NetworkedField LeftMagazineCards { get; private set; }
    [HideInInspector][Networked] public NetworkedField HandCards { get; private set; }
    [HideInInspector][Networked] public NetworkedField DiscardCards { get; private set; }
    [HideInInspector][Networked] public NetworkedField VoltageCards { get; private set; }
    [HideInInspector][Networked] public NetworkedField MechanismCards { get; private set; }
    [HideInInspector][Networked] public NetworkedField ReactionCards { get; private set; }

    // ChangeDetector
    private ChangeDetector changeDetector;

    public override void Spawned()
    {
        // ���[�J������BSM�������o���AVirtualGameboard�ł��鎩����o�^����B
        if (Object.HasStateAuthority)
        {
            BattleStateManager BSM = FindAnyObjectByType<BattleStateManager>();
            // StateAuthuority�������Ă���Ύ����̂��́B
            BSM.myGameboard = this;
        }
        else
        {
            BattleStateManager BSM = FindAnyObjectByType<BattleStateManager>();
            // StateAuthority�������Ă��Ȃ���Α���̂��́B
            BSM.opponentGameboard = this;
        }
    }

    public void InputLocalData()
    {
        MyPlayer.nickName = LocalPlayerData.NickName;
        MyPlayer.gunnerData = LocalPlayerData.GunnerData;
        MyPlayer.rightGunsData = LocalPlayerData.RightGunsData;
        MyPlayer.leftGunsData = LocalPlayerData.LeftGunsData;
        MyPlayer.skillData = LocalPlayerData.SkillData;
    }
}
