using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



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
    [SerializeField] private string gunnerHand;
    [SerializeField] private Sprite gunnerImage;
    [SerializeField] private SkillData[] skillArray = new SkillData[3];
    [SerializeField] private Vector3 iconVector;
    [SerializeField] private Vector3 iconScale;
    [SerializeField] private Color iconLabelColor;
    [SerializeField] private Vector3 playerScale;
    [SerializeField] private Vector3 opponentScale;
    [SerializeField] private Sprite gunnerThumbnail;
    [SerializeField] private Sprite gunnerDetailsImage;

    // 紐づいているEffectHub
    public EffectHub effectHub;

    // 整数値intと「X」を同時に扱える「ExtendedValue型」の手札値（アハトの手札値がXなので）
    public ExtendedValue gunnerHandValue = new();

    public void UpdateHandValue()
    {
        gunnerHandValue.MyStringValue = gunnerHand;
    }

    public GameManager.CARD_POOL GetCardPool() { return cardPool; }
    public string GetGunnerId() {  return gunnerId; }
    public string GetGunnerName() {  return gunnerName; }
    public string GetGunnerNameENG() {  return gunnerNameENG; }
    public string GetGunnerAbility() {  return gunnerAbility.Replace("\\n", "\n"); }
    public string GetGunnerFlavorText() {  return gunnerFlavorText.Replace("\\n", "\n"); }
    public int GetGunnerAgility() {  return gunnerAgility; }
    public Sprite GetGunnerImage() {  return gunnerImage; }
    public SkillData[] GetSkillArray() {  return skillArray; }
    public Vector3 GetIconVector() {  return iconVector; }
    public Vector3 GetIconScale() {  return iconScale; }
    public Vector3 GetPlayerScale() { return playerScale; }
    public Vector3 GetOpponentScale() { return opponentScale; }
    public Color GetIconLabelColor() {  return iconLabelColor; }

    public SkillData SearchSkillByID(string ID)
    {
        SkillData result = null;

        foreach (var skill in skillArray)
        {
            if (skill.GetSkillId() == ID)
            {
                result = skill;
            }
            else
            {
                continue;
            }
        }

        return result;
    }

    public Sprite GetGunnerThumbnail() { return gunnerThumbnail; }
    public Sprite GetGunnerDetails() { return gunnerDetailsImage; }
}
