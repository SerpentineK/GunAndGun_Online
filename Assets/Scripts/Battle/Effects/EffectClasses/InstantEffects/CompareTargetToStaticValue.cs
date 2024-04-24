using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompareTargetToStaticValue : InstantEffect
{
    public EffectTarget target;
    public int value;
    public CompareTargetToHubValue.MethodOfCompare methodOfCompare = CompareTargetToHubValue.MethodOfCompare.ValueEqualsOrIsSmallerThanTarget;
    public ValuesToReferTo returnResultTo = ValuesToReferTo.OperateOnActivationBool;
    public bool resetOperationBoolsOnResolveEnd = true;

    public override void Resolve()
    {
        var actualTarget = EffectManager.instance.TargetDictionary[target];
        NumeralValue actualTarget_asNumeral;
        switch (methodOfCompare)
        {
            case CompareTargetToHubValue.MethodOfCompare.ValueEqualsOrIsBiggerThanTarget:
                actualTarget_asNumeral = actualTarget as NumeralValue;
                if (value >= actualTarget_asNumeral.value)
                {
                    EffectManager.instance.HubDictionary[returnResultTo] = true;
                }
                else
                {
                    EffectManager.instance.HubDictionary[returnResultTo] = false;
                }
                break;
            case CompareTargetToHubValue.MethodOfCompare.ValueEqualsOrIsSmallerThanTarget:
                actualTarget_asNumeral = actualTarget as NumeralValue;
                if (value <= actualTarget_asNumeral.value)
                {
                    EffectManager.instance.HubDictionary[returnResultTo] = true;
                }
                else
                {
                    EffectManager.instance.HubDictionary[returnResultTo] = false;
                }
                break;
        }
    }
}
