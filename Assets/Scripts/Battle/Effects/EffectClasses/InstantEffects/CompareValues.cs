using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompareValues : InstantEffect
{
    public enum MethodOfCompare
    {
        None,
        FirstEqualsOrIsSmallerThanSecond,
        FirstIsSmallerThanSecond,
        FirstContainsSecond,
        FirstIsSameObjectAsSecond,
        FirstIsDifferentObjectFromSecond,
    }
    public ValuesToReferTo value01;
    public ValuesToReferTo value02;
    public MethodOfCompare methodOfCompare;
    public ValuesToReferTo returnResultToValue = ValuesToReferTo.OperateOnActivationBool;
    public bool resetOperationBoolsOnResolveEnd = true;
}
