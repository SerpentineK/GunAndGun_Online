using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiscernDominantCardType : InstantEffect
{
    public ValuesToReferTo enterResultTo;
    // public EffectTarget targetField;

    public override void Resolve()
    {
        // Field field = EffectManager.instance.TargetDictionary[targetField] as Field;
        Field field = EffectManager.instance.myself.FM.hand;

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

        Debug.Log("result: " + result.ToString());

        Debug.Log("HubDictionary[enterResultTo]: " + EffectManager.instance.HubDictionary[enterResultTo].ToString());

        EffectManager.instance.HubDictionary[enterResultTo] = result;

        Debug.Log("HubDictionary[enterResultTo]: " + EffectManager.instance.HubDictionary[enterResultTo].ToString());

        Debug.Log("Hub's value: " + EffectManager.instance.resolvingHub.cardTypeResult.ToString());
    }
}
