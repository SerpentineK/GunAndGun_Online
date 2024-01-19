using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUsage : AbstractEffect_Select
{
    public enum MethodOfSelect
    {
        None,
        UseOrNot,
        WhichEffectToUse
    }
    public MethodOfSelect methodOfSelect;
    [TextArea(2,10)]
    public string textToDisplay;
    public List<Effect> effects;
    public bool resolveEffectAfterSelection;
}
