using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Player opponent;

    [SerializeField] private GunnerData opponentGunner = null;
    [SerializeField] private GunsData opponentGun01 = null;
    [SerializeField] private GunsData opponentGun02 = null;
    [SerializeField] private SkillData opponentSkill = null;
    [SerializeField] private GunnerData playerGunner = null;
    [SerializeField] private GunsData playerGun01 = null;
    [SerializeField] private GunsData playerGun02 = null;
    [SerializeField] private SkillData playerSkill = null;

    [SerializeField] private EffectHub experimentalHub;

    public void InitializePlayers()
    {
        player.gunnerData = playerGunner;
        player.rightGunsData = playerGun01;
        player.leftGunsData = playerGun02;
        player.skillData = playerSkill;
        player.InputPlayerData();

        opponent.gunnerData = opponentGunner;
        opponent.rightGunsData = opponentGun01;
        opponent.leftGunsData = opponentGun02;
        opponent.skillData = opponentSkill;
        opponent.InputPlayerData();
        
        player.FM.CreateFullDeck(player.leftGun.data);
        player.FM.CreateFullDeck(player.rightGun.data);
        player.FM.ShuffleDeck(player.FM.leftDeck);
        player.FM.ShuffleDeck(player.FM.rightDeck);
    }

    public void StartGame()
    {
        player.DrawCardsAsRule(player.FM.leftDeck);
    }

    public void TestExperimentalHub()
    {
        player.EM.UnpackEffectHub(experimentalHub);
    }
}
