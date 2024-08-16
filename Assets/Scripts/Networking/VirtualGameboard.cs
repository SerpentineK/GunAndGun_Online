using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

// このクラスはプレイヤーのリソースすべてを管理する仮想盤面を表す。
// プレイヤー1人につきこのクラスのインスタンスは1つであり、ネットワーク上では共有オブジェクトとして扱う。
public class VirtualGameboard : NetworkBehaviour
{

    // プレイヤー名
    [Networked] public NetworkString<_16> NickName { get; private set; }

    // プレイヤーに対応するPlayerオブジェクト
    [Networked] public Player MyPlayer { get; private set; }

    // 各領域NetworkedFieldのNetworkedプロパティ。
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
        // ローカル環境のBSMを見つけ出し、VirtualGameboardである自分を登録する。
        if (Object.HasStateAuthority)
        {
            BattleStateManager BSM = FindAnyObjectByType<BattleStateManager>();
            // StateAuthuorityを持っていれば自分のもの。
            BSM.myGameboard = this;
        }
        else
        {
            BattleStateManager BSM = FindAnyObjectByType<BattleStateManager>();
            // StateAuthorityを持っていなければ相手のもの。
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
