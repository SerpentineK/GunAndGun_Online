using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Boss : Entity
{
    // StartSceneにて選択されたデータ
    public BossData data;
    public GunsData rightGunsData;
    public GunsData leftGunsData;
    public BossStageData stageData;

    // ボスデッキのデータ
    public BossCardDatabase bossDeck;

    // (銃士としての)ボスに帰属する各種オブジェクト
    [SerializeField] private SpriteRenderer bossPortraitImage;
    [SerializeField] private TMP_Text bossTitleDisplay;
    [SerializeField] private TMP_Text bossNameDisplay;
    [SerializeField] private TMP_Text bossLevelDisplay;
    [SerializeField] private TMP_Text bossLevelLegend;

    // (プレイヤーとしての)ボスに帰属する各種オブジェクト
    public BossStage bossStage;
    public Gun rightGun;
    public Gun leftGun;

    // 各種マネージャー
    public BossFieldManager FM;

    // (銃士としての)ボスの各種数値
    [HideInInspector] public int bossLevel = 1;
    [HideInInspector] public int[] bossLevelBorders;
    [HideInInspector] public int bossHand;
    [HideInInspector] public int bossAgility;
    [HideInInspector] public int bossReload;

    // ターンが渡っているか否か
    public bool isTurn;

    // ターン終了時に起こる手札補充の際、追加ドローの権利を有しているか
    // (そのターン中にボスカードを使うと権利は消滅する)
    // (ゲーム開始時のドローは追加ドローなしなので初期値はfalse)
    public bool hasAdditionalDraw = false;

    public void InputBossData()
    {
        bossPortraitImage.sprite = data.GetBossGraphics();
        bossNameDisplay.SetText(data.GetBossName());
        bossHand = data.GetBossHand();
        bossAgility = data.GetBossAgility();
        bossReload = data.GetBossReload();
        bossLevel = 1;
        bossLevelBorders = data.GetBossLevelBorders();
        var bossLevelDescriptions = data.GetBossLevels();
        string descriptions = "";
        foreach (string levelDescrption in bossLevelDescriptions)
        {
            descriptions += levelDescrption + "\n";
        }
        bossLevelLegend.SetText(descriptions);
        HP = data.GetBossHP();
        bossDeck = data.GetBossDeck();

        bossStage.stageData = stageData;
        bossStage.InputStageData();
        rightGun.data = rightGunsData;
        rightGun.InputGunData();
        leftGun.data = leftGunsData;
        leftGun.InputGunData();
    }

    public void UpdateBossStatus()
    {
        bossLevelDisplay.SetText(string.Format("Lv. {0:00}", bossLevel));
        HP_Counter.SetText(string.Format("{0:00}", HP));
    }

    public void DrawCardsAsRule()
    {
        if (!hasAdditionalDraw) { FM.DrawFromDeck(bossHand, FM.bossDeckField); }
        else { FM.DrawFromDeck(bossHand + 1, FM.bossDeckField); }
        hasAdditionalDraw = true;
    }
}
