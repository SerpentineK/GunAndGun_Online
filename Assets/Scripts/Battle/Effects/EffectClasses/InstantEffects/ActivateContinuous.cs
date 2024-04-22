using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateContinuous : InstantEffect
{
    public bool giveValueToContinuous;
    public ValuesToReferTo valueToGive;
    public EventCue continuousStartCue = EventCue.Immediate;
    public ContinuousEffect continuous;
    public EndContinuous endContinuous;

    public override void Resolve()
    {
        if (giveValueToContinuous) 
        {
            
        }
        if (continuousStartCue == EventCue.Immediate) 
        {
            EffectManager.instance.activeEffects.Add(continuous);
        }
        else
        {
            DormantContinuous dormant = new(continuous, continuousStartCue);
            EffectManager.instance.dormantEffects.Add(dormant);
        }
    }
}
