using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Entity
{
    // StartSceneにて選択されたデータ
    public BossData bossData;
    public GunsData rightGunsData;
    public GunsData leftGunsData;
    public BossStageData stageData;

    // ボスデッキのデータ
    public BossCardDatabase bossDeck;

    // 各種領域

    // ボスのレベル
    public int bossLevel = 0;

    // ボスのボルテージ
}
