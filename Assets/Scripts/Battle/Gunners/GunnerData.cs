using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
[CreateAssetMenu(fileName = "Gunner", menuName = "CreateGunner")]
public class GunnerData : ScriptableObject
{
    [SerializeField] private GameManager.CARD_POOL cardPool;
    [SerializeField] private string gunnerId;
    [SerializeField] private string gunnerName;
    [SerializeField] private string gunnerNameENG;
    [TextArea(3, 10)]
    [SerializeField] private string gunnerAbility;
    [TextArea(3, 10)]
    [SerializeField] private string gunnerFlavorText;
    [SerializeField] private int gunnerAgility;
    [SerializeField] private int gunnerHand;
    [SerializeField] private Sprite gunnerImage;
    [SerializeField] private SkillData[] skillArray = new SkillData[3];
    [SerializeField] private Vector3 iconVector;
    [SerializeField] private Vector3 iconScale;
    [SerializeField] private Color iconLabelColor;
    [SerializeField] private Vector3 playerScale;
    [SerializeField] private Vector3 opponentScale;

    // •R‚Ã‚¢‚Ä‚¢‚éEffectHub
    public EffectHub effectHub;

    public GameManager.CARD_POOL GetCardPool() { return cardPool; }
    public string GetGunnerId() {  return gunnerId; }
    public string GetGunnerName() {  return gunnerName; }
    public string GetGunnerNameENG() {  return gunnerNameENG; }
    public string GetGunnerAbility() {  return gunnerAbility.Replace("\\n", "\n"); }
    public string GetGunnerFlavorText() {  return gunnerFlavorText.Replace("\\n", "\n"); }
    public int GetGunnerAgility() {  return gunnerAgility; }
    public int GetGunnerHand() {  return gunnerHand; }
    public Sprite GetGunnerImage() {  return gunnerImage; }
    public SkillData[] GetSkillArray() {  return skillArray; }
    public Vector3 GetIconVector() {  return iconVector; }
    public Vector3 GetIconScale() {  return iconScale; }
    public Vector3 GetPlayerScale() { return playerScale; }
    public Vector3 GetOpponentScale() { return opponentScale; }
    public Color GetIconLabelColor() {  return iconLabelColor; }
}
