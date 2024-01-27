using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySelectionData : NetworkBehaviour
{
    // SSMを格納する変数（現地のSSMが欲しいのでNetworkedはつけない）
    public StartSceneManager SSM;

    // ローカルプレイヤーについての情報か否かbool（これもClientによって変わるためNetworkedはつけない）
    public bool isAboutLocalPlayer;

    // 各種Dataを格納するためのラッパークラス
    [Networked] public OnlineGunnerData MyGunner { get; set; }
    [Networked] public OnlineGunsData MyGun01 { get; set; }
    [Networked] public OnlineGunsData MyGun02 { get; set; }
    [Networked] public OnlineSkillData MySkill { get; set; }

    public override void Spawned()
    {
        // SSMを取得する
        SSM = FindAnyObjectByType<StartSceneManager>();

        // LocalPlayerがStateAuthorityか、つまりこのNetworkObjectについて状態権限を持っているかを確認する
        if (Runner.LocalPlayer == Object.StateAuthority) { isAboutLocalPlayer = true; }
        else { isAboutLocalPlayer = false; }
    }

    public override void FixedUpdateNetwork()
    {
        // 操作プレイヤー本人についての情報の場合は現地SSMからMyWhateverへ入力
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
        // 対戦相手についての情報の場合はMyWhateverから現地SSMへ入力
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
