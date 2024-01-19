using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyHeal : InstantEffect
{
    public ApplyHit.MeOrYou whoToApplyHeal;
    public bool healRefersToHub;
    public ValuesToReferTo valueToReferTo;
    public ModifyHit.MethodOfModify methodOfModify;
    public int modifyer;
    public int healToApply;
}
