using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCardsByOpponent : AbstractEffect_SelectByOpponent
{
    public SelectCards.PurposeOfCards purposeOfCards;
    public int maximumNumberOfSelection;
    public bool isForcedToSelectMax;
    public SearchCondition condition;
    public ValuesToReferTo numberOfCardsRefersToValue;
    public ValuesToReferTo returnNumberOfSelectedToValue;
}
