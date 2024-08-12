using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecideAboutTarget : InstantEffect
{
    public enum TopicToDecideOn
    {
        None,
        IsCurrentSequenceGunFire,
        IsSkillStillAvailableToUse,
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
                if (EffectManager.instance.currentSequence == EffectManager.Sequences.GunFire)
                {
                    EffectManager.instance.InputValueToHub(returnValuesTo, true);
                } 
                else
                {
                    EffectManager.instance.InputValueToHub(returnValuesTo, false);
                }
                break;
            case TopicToDecideOn.IsSkillStillAvailableToUse:
                if (SkillManager.instance.playerSkill.isAvailable)
                {
                    EffectManager.instance.InputValueToHub(returnValuesTo, true);
                }
                else
                {
                    EffectManager.instance.InputValueToHub(returnValuesTo, false);
                }
                break;
        }
    }
}