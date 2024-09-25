using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Metaphysics
{
    public class SelectionRecordManager : MonoBehaviour
    {
        [SerializeField] private GameObject singleplayerRecordPrefab;
        [SerializeField] private GameObject onlineRecordPrefab;

        [HideInInspector] public SelectionRecord_Singleplayer singleplayerRecord;

        public void StartRecording_Singleplayer(BossData data)
        {
            singleplayerRecord = Instantiate(singleplayerRecordPrefab, transform).GetComponent<SelectionRecord_Singleplayer>();
            singleplayerRecord.bossData = data;
        }
    }
}