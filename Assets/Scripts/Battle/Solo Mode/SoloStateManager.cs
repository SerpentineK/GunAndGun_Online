using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class SoloStateManager : MonoBehaviour
{
    public Player player;
    public Boss boss;

    public GunnerData playerGunner = null;
    public GunsData playerGun01 = null;
    public GunsData playerGun02 = null;
    public SkillData playerSkill = null;

    public BossData bossCharacter = null;
    public GunsData bossGun01 = null;
    public GunsData bossGun02 = null;
    public BossStageData bossStage = null;

    public void InitializeEntities()
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

        boss.data = bossCharacter;
        boss.rightGunsData = bossGun01;
        boss.leftGunsData = bossGun02;
        boss.stageData = bossStage;
        boss.InputBossData();

        // boss.FM.CreateFullDeck(boss.leftGun.data);
        // boss.FM.CreateFullDeck(boss.rightGun.data);
        // boss.FM.ShuffleDeck(boss.FM.leftDeck);
        // boss.FM.ShuffleDeck(boss.FM.rightDeck);

        // boss.FM.CreateBossDeck(boss.bossDeck);
    }

    public Entity DecideFirstEntity()
    {
        Entity firstEntity = null;
        if (player.gunner.agility < boss.bossAgility)
        {
            firstEntity = boss;
        }
        else if (player.gunner.agility > boss.bossAgility) 
        {
            firstEntity = player;
        }
        else
        {
            int playerWeight = player.rightGun.gunWeight + player.leftGun.gunWeight;
            int bossWeight = boss.rightGun.gunWeight + boss.leftGun.gunWeight;
            if (playerWeight > bossWeight) 
            {
                firstEntity = boss;
            }
            else if (playerWeight < bossWeight)
            {
                firstEntity = player;
            }
            else
            {
                int random = Random.Range(0, 2);
                if(random == 0)
                {
                    firstEntity = boss;
                }
                else
                {
                    firstEntity = player;
                }
            }
        }
        return firstEntity;
    }

    public void DealCards()
    {
        player.DrawCardsAsRule(player.FM.rightDeck);
        // boss.DrawCardsAsRule();
    }
}
