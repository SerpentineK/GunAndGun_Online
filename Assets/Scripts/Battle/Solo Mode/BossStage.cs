using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossStage : MonoBehaviour
{
    public BossStageData stageData;
    [SerializeField] private SpriteRenderer stageRenderer;
    [SerializeField] private TMP_Text stageNameDisplay;
    public string stageEffectAsText;

    public void InputStageData()
    {
        stageRenderer.sprite = stageData.GetStageGraphics();
        stageNameDisplay.SetText(stageData.GetStageName());
    }
}
