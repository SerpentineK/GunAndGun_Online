using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompareTargetToHubValue : InstantEffect
{
    public enum MethodOfCompare
    {
        None,
        ValueContainsTarget,
        ValueDoesNotContainTarget,
        ValueEqualsOrIsBiggerThanTarget,
        ValueEqualsOrIsSmallerThanTarget
    }
    public EffectTarget target;
    public ValuesToReferTo value;
    public MethodOfCompare methodOfCompare;
    public ValuesToReferTo returnResultTo = ValuesToReferTo.OperateOnActivationBool;
    public bool resetOperationBoolsOnResolveEnd = true;

    public override void Resolve()
    {
        var actualTarget = EffectManager.instance.TargetDictionary[target];
        var actualValue = EffectManager.instance.HubDictionary[value];
        object[] actualValue_asArray;
        int index;
        NumeralValue actualTarget_asNumeral;
        NumeralValue actualValue_asNumeral;
        switch (methodOfCompare)
        {
            case MethodOfCompare.ValueContainsTarget:
                actualValue_asArray = actualValue as object[];
                index = Array.IndexOf(actualValue_asArray, actualTarget);
                if (index >= 0)
                {
                    EffectManager.instance.InputValueToHub(returnResultTo, true);
                }
                else
                {
                    EffectManager.instance.InputValueToHub(returnResultTo, false);
                }
                break;
            case MethodOfCompare.ValueDoesNotContainTarget:
                actualValue_asArray = actualValue as object[];
                index = Array.IndexOf(actualValue_asArray, actualTarget);
                if (index < 0)
                {
                    EffectManager.instance.InputValueToHub(returnResultTo, true);
                }
                else
                {
                    EffectManager.instance.InputValueToHub(returnResultTo, false);
                }
                break;
            case MethodOfCompare.ValueEqualsOrIsBiggerThanTarget:
                actualTarget_asNumeral = actualTarget as NumeralValue;
                actualValue_asNumeral = actualValue as NumeralValue;
                if (actualValue_asNumeral.value >= actualTarget_asNumeral.value)
                {
                    EffectManager.instance.InputValueToHub(returnResultTo, true);
                }
                else
                {
                    EffectManager.instance.InputValueToHub(returnResultTo, false);
                }
                break;
            case MethodOfCompare.ValueEqualsOrIsSmallerThanTarget:
                actualTarget_asNumeral = actualTarget as NumeralValue;
                actualValue_asNumeral = actualValue as NumeralValue;
                if (actualValue_asNumeral.value <= actualTarget_asNumeral.value)
                {
                    EffectManager.instance.InputValueToHub(returnResultTo, true);
                }
                else
                {
                    EffectManager.instance.InputValueToHub(returnResultTo, false);
                }
                break;
        }
    }
}
