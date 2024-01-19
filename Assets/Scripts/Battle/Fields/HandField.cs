using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandField : Field
{
    public override void RegisterCard(Card card)
    {
        card.visibleCard.transform.SetParent(transform, false);
        card.visibleCard.gameObject.SetActive(true);
        card.ToggleInHand();
        base.RegisterCard(card);
    }

    public override void RemoveCard(Card card)
    {
        card.visibleCard.gameObject.SetActive(false);
        card.ToggleInHand();
        base.RemoveCard(card);
    }
}
