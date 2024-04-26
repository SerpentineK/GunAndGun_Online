using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "RuleData", menuName = "Create RuleData")]
public class RuleData : ScriptableObject
{
    public enum RuleEffect
    {
        None,
        WinWhenValid,
        LoseWhenValid,
        CannotWinWhenValid,
        CannotLoseWhenValid,
        ModifyValueWhenValid
    }

    public enum RuleCheckTrigger
    {
        None,
        AlwaysCheck,
        TurnStart,
        TurnEnd,
        TakesFatalDamageFromEffectOrFire
    }

    public enum ValidCondition
    {
        None,
        LifeIsZero,
        BothDecksAreDepleted,
        BothGunsAreDeactivated,
        LifeIsThreeOrLess,
        DeckCardsAreThreeOrLess,
        LoveCountersExist,
        DamageIsSixOrLess,
        HasFiveOrMoreShockCounters,
        HasOneOrMoreBurnCounters
    }

    public enum RuleModifyTarget
    {
        None,
        DamageTaken,
        CurrentHP
    }

    public RuleEffect ruleEffect;
    public RuleCheckTrigger checkTrigger;
    public ValidCondition validCondition;
    public RuleModifyTarget modifyTarget;
    public ModifyHit.MethodOfModify modifyMethod;
    public int modifyer;
    public bool bossIsExcluded;
    public bool soloPlayerIsExcluded;
}
