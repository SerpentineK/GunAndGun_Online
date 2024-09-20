using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossData : ScriptableObject
{
    [SerializeField] private string bossId;
    [SerializeField] private string bossName;
    [SerializeField] private string bossTitle;
    [SerializeField] private Sprite bossGraphics;
    [SerializeField] private int bossHP;
    [SerializeField] private int bossReload;
    [TextArea(2,10)]
    [SerializeField] private string[] bossAbilities;
    [SerializeField] private int bossAgility;
    [SerializeField] private int bossHand;
    [SerializeField] private string[] bossLevels;
    [SerializeField] private int[] bossLevelBorders;
    [SerializeField] private BossCardDatabase bossDeck;
    [SerializeField] private BossStageDatabase bossStages;
    [SerializeField] private Sprite bossDetails;
    [SerializeField] private Sprite bossStageDetails;
    [SerializeField] private Sprite bossDeckDetails;

    public string GetBossId() {  return bossId; }
    public string GetBossName() {  return bossName; }
    public string GetBossTitle() {  return bossTitle; }
    public Sprite GetBossGraphics() {  return bossGraphics; }
    public int GetBossHP() {  return bossHP; }
    public int GetBossReload() {  return bossReload; }
    public string[] GetBossAbilities() {  return bossAbilities; }
    public int GetBossAgility() {  return bossAgility; }
    public int GetBossHand() { return bossHand; }
    public string[] GetBossLevels() { return bossLevels; }
    public int[] GetBossLevelBorders() { return bossLevelBorders; }
    public BossCardDatabase GetBossDeck() {  return bossDeck; }
    public BossStageDatabase GetBossStages() {  return bossStages; }

    public Sprite GetBossDetails() { return bossDetails; }
    public Sprite GetBossStageDetails() { return bossStageDetails; }
    public Sprite GetBossDeckDetails() {  return bossDeckDetails; }
}
