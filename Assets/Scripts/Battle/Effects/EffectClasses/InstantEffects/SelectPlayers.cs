using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPlayers : AbstractEffect_Select
{
    public enum MethodOfSelection
    {
        None,
        SelectOnePlayer,
        SelectAnyNumberOfPlayers
    }

    public MethodOfSelection methodOfSelection;
    public ValuesToReferTo returnSelectedToValue = ValuesToReferTo.PlayerSet;
}
