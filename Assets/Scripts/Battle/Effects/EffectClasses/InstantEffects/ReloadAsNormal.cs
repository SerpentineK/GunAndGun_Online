using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadAsNormal : InstantEffect
{
    public bool selectGun = true;
    public ValuesToReferTo gunToReloadWhenSelectIsOff = ValuesToReferTo.None;
    public ValuesToReferTo cardsToReload = ValuesToReferTo.CardSet01;
}
