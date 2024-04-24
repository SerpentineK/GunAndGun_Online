using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecideAboutTarget : InstantEffect
{
    public enum TopicToDecideOn
    {
        None,
        IsCurrentSequenceGunFire,
        IsCurrentReloadByEffect,
        IsCurrentEffectNotByGunner,
        IsSkillStillAvailableToUse,
        IsTargetedCardSubjectOfTransfer,
    }

    public EffectTarget target;
    public TopicToDecideOn topicToDecideOn;
    public ValuesToReferTo returnValuesTo = ValuesToReferTo.OperateOnActivationBool;
    public bool resetOperationBoolsOnResolveEnd = true;

    public override void Resolve()
    {
        var actualTarget = EffectManager.instance.TargetDictionary[target];
        switch (topicToDecideOn)
        {
            case TopicToDecideOn.None:
                break;
            case TopicToDecideOn.IsCurrentSequenceGunFire:
                
                break;
        }
    }
}