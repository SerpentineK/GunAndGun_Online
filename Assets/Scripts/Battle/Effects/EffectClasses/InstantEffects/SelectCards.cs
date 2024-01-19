using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCards : AbstractEffect_Select
{
    public enum PurposeOfCards
    {
        None,
        DrawToHand,
        Discard,
        Reload,
        ReturnToDeck,
        TargetOfEffect,
        PutIntoVoltage,
        ReloadAsSpecial,
        ReloadAsNormal,
        MakeOpponentDrawToHand,
        MakeOpponentDiscard,
    }
    public PurposeOfCards purposeOfCards;
    public int maximumNumberOfSelection;
    public bool isForcedToSelectMax;
    public bool quitIfZeroIsSelected;
    // ï\é¶Ç≥ÇÍÇÈèåè
    public SearchCondition searchCondition = SearchCondition.All;
    // ëIëÇ≈Ç´ÇÈèåè
    public SearchCondition selectionCondition = SearchCondition.All;
    public ValuesToReferTo numberOfCardsRefersToValue;
    public ValuesToReferTo returnNumberOfSelectedToValue;
}
