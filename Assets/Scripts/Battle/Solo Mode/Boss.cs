using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Boss : Entity
{
    // StartScene�ɂđI�����ꂽ�f�[�^
    public BossData data;
    public GunsData rightGunsData;
    public GunsData leftGunsData;
    public BossStageData stageData;

    // �{�X�f�b�L�̃f�[�^
    public BossCardDatabase bossDeck;

    // (�e�m�Ƃ��Ă�)�{�X�ɋA������e��I�u�W�F�N�g
    [SerializeField] private SpriteRenderer bossPortraitImage;
    [SerializeField] private TMP_Text bossTitleDisplay;
    [SerializeField] private TMP_Text bossNameDisplay;
    [SerializeField] private TMP_Text bossLevelDisplay;
    [SerializeField] private TMP_Text bossLevelLegend;

    // (�v���C���[�Ƃ��Ă�)�{�X�ɋA������e��I�u�W�F�N�g
    public BossStage bossStage;
    public Gun rightGun;
    public Gun leftGun;

    // �e��}�l�[�W���[
    public BossFieldManager FM;

    // (�e�m�Ƃ��Ă�)�{�X�̊e�퐔�l
    [HideInInspector] public int bossLevel = 1;
    [HideInInspector] public int[] bossLevelBorders;
    [HideInInspector] public int bossHand;
    [HideInInspector] public int bossAgility;
    [HideInInspector] public int bossReload;

    // �^�[�����n���Ă��邩�ۂ�
    public bool isTurn;

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

        bossStage.stageData = stageData;
        bossStage.InputStageData();
        rightGun.data = rightGunsData;
        rightGun.InputGunData();
        leftGun.data = leftGunsData;
        leftGun.InputGunData();
        bossDeck = data.GetBossDeck();
    }

    public void UpdateBossStatus()
    {
        bossLevelDisplay.SetText(string.Format("Lv. {0:00}", bossLevel));
        HP_Counter.SetText(string.Format("{0:00}", HP));
    }
}
