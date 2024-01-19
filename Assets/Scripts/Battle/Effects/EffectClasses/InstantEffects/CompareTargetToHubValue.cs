using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompareTargetToHubValue : InstantEffect
{
    public enum MethodOfCompare
    {
        None,
        ValueIncludesTarget,
        ValueDoesNotContainTarget,
        ValueEqualsOrIsBiggerThanTarget,
        ValueEqualsOrIsSmallerThanTarget
    }
    public EffectTarget target;
    public ValuesToReferTo value;
    public MethodOfCompare methodOfCompare;
    public ValuesToReferTo returnResultTo = ValuesToReferTo.OperateOnActivationBool;
    public bool resetOperationBoolsOnResolveEnd = true;
}
