using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractEffect_Transfer : InstantEffect
{
    public bool transferIsForOpponent;
    [HideInInspector]
    public Card[] cardsToTransfer;
}