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
    [SerializeField] private string skillCost;
    [SerializeField] private string skillName;
    [TextArea(3,10)]
    [SerializeField] private string skillEffectText;
    [TextArea(3, 10)]
    [SerializeField] private string skillFlavorText;
    [SerializeField] private Sprite skillGraphics;

    // 紐づいているEffectHub
    public EffectHub effectHub;

    // 整数値intと「X」を同時に扱える「ExtendedValue型」のコスト値
    public ExtendedValue skillCostValue = new();

    public void UpdateCostValue()
    {
        skillCostValue.MyStringValue = skillCost;
    }

    public CardPool GetCardPool() { return cardPool; }
    public string GetSkillId() { return skillId; }
    public string GetSkillName() { return skillName; }
    public string GetSkillEffectText() { return skillEffectText.Replace("\\n","\n"); }
    public string GetSkillFlavorText() { return skillFlavorText.Replace("\\n", "\n"); }
    public Sprite GetSkillGraphics() { return skillGraphics; }

}
