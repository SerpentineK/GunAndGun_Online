using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardCards : AbstractEffect_Transfer
{
    public ValuesToReferTo cardsToReferTo = ValuesToReferTo.CardSet01;

    public override void Resolve()
    {
        Card[] targetCards = EffectManager.instance.HubDictionary[cardsToReferTo] as Card[];
        foreach (Card card in targetCards)
        {
            FieldManager.instance.TransferCard(card.currentField, FieldManager.instance.discard, card);
        }
    }
}
