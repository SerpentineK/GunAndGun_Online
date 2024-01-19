using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractEffect_Select : InstantEffect
{
    public enum MethodToDealWithExceptions
    {
        None,
        QuitIfNoCandidate,
        PassIfNoCandidate
    }
    public EffectTarget[] targets;
    public ValuesToReferTo whereToInput = ValuesToReferTo.CardSet01;
    public bool selectAll;
    public bool isRandom;
    public bool valueInputIsAdditive;
    public MethodToDealWithExceptions methodToDealWithExceptions;
}
