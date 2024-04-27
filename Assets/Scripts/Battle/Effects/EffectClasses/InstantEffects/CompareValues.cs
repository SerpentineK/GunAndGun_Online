using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompareValues : InstantEffect
{
    public enum MethodOfCompare
    {
        None,
        FirstEqualsOrIsSmallerThanSecond,
        FirstIsSmallerThanSecond,
        FirstContainsSecond,
        FirstIsSameObjectAsSecond,
        FirstIsDifferentObjectFromSecond,
    }
    public ValuesToReferTo value01;
    public ValuesToReferTo value02;
    public MethodOfCompare methodOfCompare;
    public ValuesToReferTo returnResultToValue = ValuesToReferTo.OperateOnActivationBool;
    public bool resetOperationBoolsOnResolveEnd = true;

    public override void Resolve()
    {
        var actualValue01 = EffectManager.instance.HubDictionary[value01];
        var actualValue02 = EffectManager.instance.HubDictionary[value02];
        NumeralValue value01_asNumeral = actualValue01 as NumeralValue;
        NumeralValue value02_asNumeral = actualValue02 as NumeralValue;
        Card[] value01_asArray = actualValue01 as Card[];
        Card value02_asCard = actualValue02 as Card;
        switch (methodOfCompare)
        {
            case MethodOfCompare.FirstEqualsOrIsSmallerThanSecond:
                if (value01_asNumeral.value <= value02_asNumeral.value)
                {
                    EffectManager.instance.InputValueToHub(returnResultToValue, true);
                }
                else
                {
                    EffectManager.instance.InputValueToHub(returnResultToValue, false);
                }
                break;
            case MethodOfCompare.FirstIsSmallerThanSecond:
                if (value01_asNumeral.value < value02_asNumeral.value)
                {
                    EffectManager.instance.InputValueToHub(returnResultToValue, true);
                }
                else
                {
                    EffectManager.instance.InputValueToHub(returnResultToValue, false);
                }
                break;
            case MethodOfCompare.FirstContainsSecond:
                if (Array.IndexOf(value01_asArray, value02_asCard) >= 0)
                {
                    EffectManager.instance.InputValueToHub(returnResultToValue, true);
                }
                else
                {
                    EffectManager.instance.InputValueToHub(returnResultToValue, false);
                }
                break;
            case MethodOfCompare.FirstIsSameObjectAsSecond:
                if (ReferenceEquals(actualValue01, actualValue02))
                {
                    EffectManager.instance.InputValueToHub(returnResultToValue, true);
                }
                else
                {
                    EffectManager.instance.InputValueToHub(returnResultToValue, false);
                }
                break;
            case MethodOfCompare.FirstIsDifferentObjectFromSecond:
                if (!ReferenceEquals(actualValue01, actualValue02))
                {
                    EffectManager.instance.InputValueToHub(returnResultToValue, true);
                }
                else
                {
                    EffectManager.instance.InputValueToHub(returnResultToValue, false);
                }
                break;

        }
    }
}
