using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Effect;

public class ReloadAsSpecial : InstantEffect
{
    public bool selectGun = true;
    public ValuesToReferTo gunToReloadWhenSelectIsOff = ValuesToReferTo.None;
    public ValuesToReferTo cardsToReload = ValuesToReferTo.CardSet01;
}
