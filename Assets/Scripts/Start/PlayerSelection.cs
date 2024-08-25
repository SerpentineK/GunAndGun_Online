using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;


// �e��Data�^��ScriptableObject�h���Ȃ̂ł��ꎩ�̂�Networked�v���p�e�B�ɂ͂ł��Ȃ��B
// �����ŁA��xID��string�ŋ��L���Ă��炻��ID�������Ă���Data�����[�J���Ō�������B
// ���łɑI�����̊Ǘ������̃I�u�W�F�N�g�ɒS�킹�邱�Ƃɂ���B

public class PlayerSelection : NetworkBehaviour
{
    // �e��Data�^�̕ϐ��B
    // ���̕ϐ��ɃC���X�^���X����������Ƃ��A�����ɂ��̃C���X�^���X��ID��Networked�v���p�e�B�ɓ���B
    private GunnerData _selectedGunnerData = null;
    public GunnerData SelectedGunnerData {
        get 
        {  
            return _selectedGunnerData;
        }
        set 
        {  
            GunnerID = value?.GetGunnerId();
            _selectedGunnerData = value;
        }
    }

    private GunsData _selectedGun01Data = null;
    public GunsData SelectedGun01Data
    {
        get
        {
            return _selectedGun01Data;
        }
        set
        {
            Gun01ID = value?.GetGunId();
            _selectedGun01Data = value;
        }
    }

    private GunsData _selectedGun02Data = null;
    public GunsData SelectedGun02Data
    {
        get
        {
            return _selectedGun02Data;
        }
        set
        {
            Gun02ID = value?.GetGunId();
            _selectedGun02Data = value;
        }
    }

    private SkillData _selectedSkillData = null;
    public SkillData SelectedSkillData
    {
        get
        {
            return _selectedSkillData;
        }
        set
        {
            SkillID = value?.GetSkillId();
            _selectedSkillData = value;
        }
    }

    // ID�`�B�p��Networked�v���p�e�B�B
    [Networked] public string GunnerID { get; private set; }
    [Networked] public string Gun01ID { get; private set; }
    [Networked] public string Gun02ID { get; private set; }
    [Networked] public string SkillID { get; private set; }

    // �I���̎�Ԃ�����Ă��Ă��邩�ۂ���bool�l�B
    public bool IsSelectionTurn;

    // Spawned�Ń��[�J������StateAuthority�����̃I�u�W�F�N�g�ɂ��Ď����Ă����SSM��mySelection�ɁA�����Ă��Ȃ����theirSelection�ɓ����B
    public override void Spawned()
    {
        if (Object.HasStateAuthority) { StartStateManager.instance.mySelection = this; }
        else { StartStateManager.instance.theirSelection = this; }
    }
}
