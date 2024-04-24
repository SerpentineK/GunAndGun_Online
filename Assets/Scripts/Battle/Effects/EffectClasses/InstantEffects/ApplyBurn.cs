using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyBurn : InstantEffect
{
    public ApplyHit.MeOrYou whoToApply;
    public int numberOfBurn;

    private Player victim;

    public override void Resolve()
    {
        if (whoToApply == ApplyHit.MeOrYou.Player)
        {
            victim = EffectManager.instance.myself;
        }
        else
        {
            victim = EffectManager.instance.opponent;
        }
        victim.burnCounters += numberOfBurn;
    }
}
