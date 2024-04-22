using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DormantContinuous : Object
{
    public ContinuousEffect continuousEffect;
    public Effect.EventCue awakenCue;

    public DormantContinuous(ContinuousEffect continuous, Effect.EventCue cue)
    {
        continuousEffect = continuous;
        awakenCue = cue;
    }
}
