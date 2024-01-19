using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "BossStageDatabase", menuName = "CreateBossStage_Database", order = 2)]
public class BossStageDatabase : ScriptableObject
{
    [SerializeField] private BossData bossAttached;
    [SerializeField] private List<BossStageData> stageDataLists = new List<BossStageData>();

    public List<BossStageData> GetStageDataLists() { return stageDataLists; }
}
