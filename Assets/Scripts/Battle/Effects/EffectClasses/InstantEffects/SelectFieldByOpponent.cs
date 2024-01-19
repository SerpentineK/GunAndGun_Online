using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectFieldByOpponent : AbstractEffect_SelectByOpponent
{
    public enum PurposeOfField
    {
        None,
        DrawFromField,
        DiscardFromField,
        PutToVoltFromField,
        ReloadFromField,
        TargetOfEffect
    }
    public enum FieldCondition
    {
        None,
        All,
        HasMoreThanOneCard,
        IsNotForbiddenDraw
    }

    public PurposeOfField purposeOfField = PurposeOfField.None;
    public int numberOfFields;
    public FieldCondition condition = FieldCondition.HasMoreThanOneCard;
}
