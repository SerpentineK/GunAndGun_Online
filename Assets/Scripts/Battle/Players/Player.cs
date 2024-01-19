using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    // プレイヤーに帰属するマネージャー
    public FieldManager FM;
    public EffectManager EM;
    public SkillManager SM;

    // StartSceneにて選択されたデータ
    public GunnerData gunnerData;
    public GunsData rightGunsData;
    public GunsData leftGunsData;
    public SkillData skillData;

    // 視点のプレイヤーか否か
    public bool isProtagonist;

    // ターンプレイヤーか否か
    public bool isTurn;

    // ヒットポイント
    public int HP = 30;
    public TMP_Text HP_Counter;

    // 手札枚数
    public int handNum;

    // このプレイヤーの銃士、機銃、技能オブジェクト
    public Gunner gunner;
    public Gun rightGun;
    public Gun leftGun;
    public Skill skill;

    public void InputPlayerData() 
    {
        gunner.data = gunnerData;
        gunner.InputGunnerData();
        rightGun.data = rightGunsData;
        rightGun.InputGunData();
        leftGun.data = leftGunsData;
        leftGun.InputGunData();
        skill.data = skillData;
        skill.InputSkillData();
        HP_Counter.SetText(string.Format("{0:00}", HP));
        handNum = gunner.hand;
    }
 
    
    public void DrawCardsAsRule()
    {
        int currentHandNum = FM.hand.cardCount;
        int numToDraw = handNum - currentHandNum;
        if (numToDraw > 0)
        {
            FM.DrawFromDeck(numToDraw,FM.leftDeck);
        }
    }
}
