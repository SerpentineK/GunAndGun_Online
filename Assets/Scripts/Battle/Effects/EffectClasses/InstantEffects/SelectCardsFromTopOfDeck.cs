using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCardsFromTopOfDeck : AbstractEffect_Select
{
    public ValuesToReferTo deckToReferTo = ValuesToReferTo.Field01;
    public int numberOfCards;
    public ValuesToReferTo numberOfCardsRefersToValue;
    public ValuesToReferTo returnNumberOfSelectedToValue;
    public bool playerSelectsNumberOfCards;
    public bool quitIfZeroIsSelected;
}
