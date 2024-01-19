using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Boss", menuName = "CreateBoss", order = 2)]
public class BossData : ScriptableObject
{
    [SerializeField] private string bossId;
    [SerializeField] private string bossName;
    [SerializeField] private string bossTitle;
    [SerializeField] private Sprite bossGraphics;
    [SerializeField] private int bossLife;
    [SerializeField] private int bossReload;
    [TextArea(2,10)]
    [SerializeField] private string[] bossAbilities;
    [SerializeField] private int bossAgility;
    [SerializeField] private int bossHand;
    [SerializeField] private string[] bossLevels;
    [SerializeField] private BossCardDatabase bossDeck;
    [SerializeField] private BossStageDatabase bossStages;
    
    public string GetBossId() {  return bossId; }
    public string GetBossName() {  return bossName; }
    public string GetBossTitle() {  return bossTitle; }
    public Sprite GetBossGraphics() {  return bossGraphics; }
    public int GetBossLife() {  return bossLife; }
    public int GetBossReload() {  return bossReload; }
    public string[] GetBossAbilities() {  return bossAbilities; }
    public int GetBossAgility() {  return bossAgility; }
    public int GetBossHand() { return bossHand; }
    public string[] GetBossLevels() { return bossLevels; }
    public BossCardDatabase GetBossDeck() {  return bossDeck; }
    public BossStageDatabase GetBossStages() {  return bossStages; }
}
