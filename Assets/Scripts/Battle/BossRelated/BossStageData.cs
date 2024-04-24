using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossStageData : ScriptableObject
{
    [SerializeField] private string stageId;
    [SerializeField] private string stageName;
    [TextArea(3,10)]
    [SerializeField] private string stageAbility;
    [SerializeField] private Sprite stageLogo;
    [SerializeField] private Sprite stageGraphics;

    public string GetStageId() { return stageId; }
    public string GetStageName() {  return stageName; }
    public string GetStageAbility() {  return stageAbility; }
    public Sprite GetStageLogo() {  return stageLogo; }
    public Sprite GetStageGraphics() {  return stageGraphics; }
}
