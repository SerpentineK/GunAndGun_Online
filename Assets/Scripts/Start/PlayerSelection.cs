using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;


// 各種Data型はScriptableObject派生なのでそれ自体をNetworkedプロパティにはできない。
// そこで、一度IDをstringで共有してからそのIDを持っているDataをローカルで検索する。
// ついでに選択順の管理もこのオブジェクトに担わせることにする。

public class PlayerSelection : NetworkBehaviour
{
    // 各種Data型の変数。
    // この変数にインスタンスが代入されるとき、同時にそのインスタンスのIDがNetworkedプロパティに入る。
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

    // ID伝達用のNetworkedプロパティ。
    [Networked] public string GunnerID { get; private set; }
    [Networked] public string Gun01ID { get; private set; }
    [Networked] public string Gun02ID { get; private set; }
    [Networked] public string SkillID { get; private set; }

    // 選択の手番が回ってきているか否かのbool値。
    public bool IsSelectionTurn;

    // Spawnedでローカル環境がStateAuthorityをこのオブジェクトについて持っていればSSMのmySelectionに、持っていなければtheirSelectionに入れる。
    public override void Spawned()
    {
        if (Object.HasStateAuthority) { StartStateManager.instance.mySelection = this; }
        else { StartStateManager.instance.theirSelection = this; }
    }
}
