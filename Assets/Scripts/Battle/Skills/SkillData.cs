using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Security.Cryptography.X509Certificates;


[Serializable]
[CreateAssetMenu(fileName = "Skill", menuName = "CreateSkill")]
public class SkillData : ScriptableObject
{
    public enum CardPool
    {
        GunAndGun,
        OverHeat,
        WShout,
        UltraBommy
    }

    [SerializeField] private CardPool cardPool = CardPool.UltraBommy;
    [SerializeField] private string skillId;
    [SerializeField] private int skillCost;
    [SerializeField] private string skillName;
    [TextArea(3,10)]
    [SerializeField] private string skillEffectText;
    [TextArea(3, 10)]
    [SerializeField] private string skillFlavorText;
    [SerializeField] private Sprite skillGraphics;

    // •R‚Ã‚¢‚Ä‚¢‚éEffectHub
    public EffectHub effectHub;

    public CardPool GetCardPool() { return cardPool; }
    public string GetSkillId() {  return skillId; }
    public int GetSkillCost() {  return skillCost; }
    public string GetSkillName() {  return skillName; }
    public string GetSkillEffectText() {  return skillEffectText.Replace("\\n","\n"); }
    public string GetSkillFlavorText() {  return skillFlavorText.Replace("\\n", "\n"); }
    public Sprite GetSkillGraphics() { return skillGraphics; }

}
