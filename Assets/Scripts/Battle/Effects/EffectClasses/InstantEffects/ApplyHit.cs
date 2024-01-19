using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyHit : InstantEffect
{
    public enum MeOrYou
    {
        None,
        Player,
        Opponent
    }

    public MeOrYou whoToApplyHit;
    public bool hitRefersToHub;
    public ValuesToReferTo valueToReferTo;
    public ModifyHit.MethodOfModify methodOfModify;
    public int modifyer;
    public int hitToApply;
}

