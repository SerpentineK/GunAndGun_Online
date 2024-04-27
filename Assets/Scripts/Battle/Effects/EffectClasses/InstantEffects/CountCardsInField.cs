using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountCardsInField : InstantEffect
{
    public EffectTarget fieldToCount;
    public ModifyHit.MethodOfModify methodOfModify;
    public int modifyer;
    public ValuesToReferTo returnResultTo = ValuesToReferTo.NumeralValue01;

    private int result;

    public override void Resolve()
    {
        Field field = EffectManager.instance.TargetDictionary[fieldToCount] as Field;
        result = field.cardCount;
        
        switch (methodOfModify)
        {
            case ModifyHit.MethodOfModify.None:
                break;
            case ModifyHit.MethodOfModify.Add:
                result += modifyer;
                break;
            case ModifyHit.MethodOfModify.Multiply:
                result *= modifyer;
                break;
            case ModifyHit.MethodOfModify.Divide:
                result /= modifyer;
                break;
        }

        NumeralValue numeralValue = new();
        numeralValue.value = result;

        EffectManager.instance.InputValueToHub(returnResultTo, numeralValue);
    }
}
