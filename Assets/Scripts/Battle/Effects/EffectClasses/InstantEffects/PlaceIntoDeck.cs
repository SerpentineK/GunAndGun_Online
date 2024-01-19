using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceIntoDeck : AbstractEffect_Transfer
{
    public enum PositionInDeck
    {
        None,
        Top,
        Bottom
    }
    public ValuesToReferTo cardsToReferTo = ValuesToReferTo.CardSet01;
    public PositionInDeck positionInDeck;
    public bool orderIsRandom;
    public bool shuffleDeckAfterPlacement;
}
