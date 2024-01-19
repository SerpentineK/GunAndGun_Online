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
}
