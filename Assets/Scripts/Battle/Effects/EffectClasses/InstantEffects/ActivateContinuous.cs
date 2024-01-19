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
}
