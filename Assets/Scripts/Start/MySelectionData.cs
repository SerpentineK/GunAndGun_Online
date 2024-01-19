using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySelectionData : NetworkBehaviour
{
    public static bool GunnerNetworkToken { get; private set; } = false;
    public static bool FirstGunNetworkToken { get; private set; } = false;
    public static bool SecondGunNetworkToken { get; private set; } = false;
    public static bool SkillNetworkToken { get; private set; } = false;

    public StartSceneManager SSM;

    // [Networked]
    public GunnerData MyGunner { get; set; }

    // [Networked]
    public GunsData myGun01 { get; set; }

    // [Networked]
    public GunsData myGun02 { get; set; }

    // [Networked]
    public SkillData MySkill { get; set; }

    public void Update()
    {
        if (SSM.GunnerToken && !GunnerNetworkToken)
        {
            MyGunner = SSM.playerGunner;
            GunnerNetworkToken = true;
        }
    }
}
