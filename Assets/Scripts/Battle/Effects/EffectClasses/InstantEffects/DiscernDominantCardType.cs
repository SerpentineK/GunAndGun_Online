using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiscernDominantCardType : InstantEffect
{
    public ValuesToReferTo enterResultTo;
    public ValuesToReferTo enterNumOfCardsTo;
    public EffectTarget targetField;

    public override void Resolve()
    {
        Field field = EffectManager.instance.TargetDictionary[targetField] as Field;

        Dictionary<CardData.CardType, int> dict = new()
        {
            { CardData.CardType.Action, 0 },
            { CardData.CardType.Reaction, 0 },
            { CardData.CardType.Mechanism, 0 },
            { CardData.CardType.SpecialBullet, 0 },
        };

        foreach(Card card in field.cardList)
        {
            dict[card.cardType]++;
        }

        int maxNum = dict.Values.Max();
        CardData.CardType result = CardData.CardType.Other;

        foreach (var item in dict)
        {
            if(item.Value == maxNum)
            {
                result = item.Key;
                break;
            }
        }

        NumeralValue numeral = new();
        numeral.value = maxNum;

        EffectManager.instance.InputValueToHub(enterResultTo, result);
        EffectManager.instance.InputValueToHub(enterNumOfCardsTo, numeral);
    }
}
