using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class SoloStateManager : MonoBehaviour
{
    [SerializeField] private Player player;

    public BossData bossCharacter = null;
    public GunsData bossGun01 = null;
    public GunsData bossGun02 = null;
    public BossStageData bossStage = null;
    public GunnerData playerGunner = null;
    public GunsData playerGun01 = null;
    public GunsData playerGun02 = null;
    public SkillData playerSkill = null;

    public void InitializePlayers()
    {
        player.gunnerData = playerGunner;
        player.rightGunsData = playerGun01;
        player.leftGunsData = playerGun02;
        player.skillData = playerSkill;
        player.InputPlayerData();

        

        player.FM.CreateFullDeck(player.leftGun.data);
        player.FM.CreateFullDeck(player.rightGun.data);
        player.FM.ShuffleDeck(player.FM.leftDeck);
        player.FM.ShuffleDeck(player.FM.rightDeck);
    }
}
