using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundDownValue : InstantEffect
{
    public enum MethodOfCalculation
    {
        None,
        MakeSumSmallerThanCertainInt,
        MakeSumEqualCertainInt,
        MakeValuesDifferForCertainInt,
    }
    public enum MethodOfResolve
    {
        None,
        AlterValue01,
        AlterValue02,
    }
    public MethodOfCalculation methodOfCalculation;
    public MethodOfResolve methodOfResolve;
    public bool maxValueFollowsTarget = false;
    public int maxValue;
    public EffectTarget target;
    public ValuesToReferTo value01;
    public ValuesToReferTo value02;
    public bool quitIfEitherValueEqualsOrIsUnderZero = true;
}
