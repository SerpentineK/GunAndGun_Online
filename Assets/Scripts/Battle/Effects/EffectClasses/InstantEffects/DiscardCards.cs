using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardCards : AbstractEffect_Transfer
{
    public ValuesToReferTo cardsToReferTo = ValuesToReferTo.CardSet01;

    public override void Resolve()
    {
        Card[] targetCards = EffectManager.instance.HubDictionary[cardsToReferTo] as Card[];
        if (transferIsForOpponent)
        {

        }
        else
        {
            foreach (Card card in targetCards)
            {
                PlayerFieldManager.instance.TransferCard(card.currentField, PlayerFieldManager.instance.discard, card);
            }
        }
    }
}
