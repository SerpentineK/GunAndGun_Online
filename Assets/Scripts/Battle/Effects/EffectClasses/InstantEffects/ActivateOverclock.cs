using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOverclock : InstantEffect
{
    public ValuesToReferTo gunToApplyOverclock = ValuesToReferTo.GunSet01;

    public override void Resolve()
    {
        Gun[] overclockTargetList =  EffectManager.instance.HubDictionary[gunToApplyOverclock] as Gun[];
        Gun target = overclockTargetList[0];
        if (!target.isOverclocked) 
        {
            target.isOverclocked = true;
            if (!target.isPlayer) { EffectManager.instance.RecieveCue(EventCue.OpponentOverclock); }
            else { EffectManager.instance.SendCue(EventCue.OpponentOverclock); }
        }
        else
        {
            EffectManager.instance.resolvingHub.childrenOperateOnActivation = false;
        }
    }
}
