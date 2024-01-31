using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class StandbyView : MonoBehaviour
{
    public InfoDisplay playerDisplay;
    public InfoDisplay opponentDisplay;

    public GunnerData playerGunner = null;
    public GunsData playerGun01 = null;
    public GunsData playerGun02 = null;
    public GunnerData opponentGunner = null;
    public GunsData opponentGun01 = null;
    public GunsData opponentGun02 = null;

    public void UpdateStandbyInfo()
    {
        playerDisplay.gunnerData = playerGunner;
        playerDisplay.gun01Data = playerGun01;
        playerDisplay.gun02Data = playerGun02;
        playerDisplay.InputData();
        
        opponentDisplay.gunnerData = opponentGunner;
        opponentDisplay.gun01Data = opponentGun01;
        opponentDisplay.gun02Data = opponentGun02;
        opponentDisplay.InputData();
    }
}
