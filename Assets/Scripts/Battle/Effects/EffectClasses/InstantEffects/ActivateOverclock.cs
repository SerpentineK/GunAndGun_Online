using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOverclock : InstantEffect
{
    public ValuesToReferTo gunToApplyOverclock = ValuesToReferTo.GunSet01;
    public ValuesToReferTo returnResultTo = ValuesToReferTo.OperateOnActivationBool;
    public Overclock overclockEffect;
}
