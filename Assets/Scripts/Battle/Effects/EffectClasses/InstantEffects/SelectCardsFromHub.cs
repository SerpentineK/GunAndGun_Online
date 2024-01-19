using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SelectCards;

public class SelectCardsFromHub : AbstractEffect_Select
{
    public ValuesToReferTo selectFromValue;
    public bool deleteCardsFromOldValue = true;
    public PurposeOfCards purposeOfCards;
    public int maximumNumberOfSelection;
    public bool isForcedToSelectMax;
    public bool canSelectAnyNumberOfCards;
    public bool quitIfZeroIsSelected;
    // �\����������
    public SearchCondition searchCondition = SearchCondition.All;
    // �I���ł������
    public SearchCondition selectionCondition = SearchCondition.All;
}
