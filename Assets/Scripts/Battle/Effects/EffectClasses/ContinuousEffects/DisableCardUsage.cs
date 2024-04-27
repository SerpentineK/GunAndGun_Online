using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCardUsage : AbstractEffect_Disable
{
    public enum BypassCondition 
    {
        None,
        LoadedByEffect,
        FirstUsageInTurn,
        BothOpponentGunsAvailable,
        IsNotDominantInHand
    }
    public BypassCondition bypassCondition;
    public bool disableIsAboutSameNameCards;
    public Card[] cardsToDisable;
}
