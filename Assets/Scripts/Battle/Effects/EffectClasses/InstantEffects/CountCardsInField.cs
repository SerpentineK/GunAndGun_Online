using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountCardsInField : InstantEffect
{
    public EffectTarget fieldToCount;
    public ModifyHit.MethodOfModify methodOfModify;
    public int modifyer;
    public ValuesToReferTo returnResultTo = ValuesToReferTo.Int01;
}
