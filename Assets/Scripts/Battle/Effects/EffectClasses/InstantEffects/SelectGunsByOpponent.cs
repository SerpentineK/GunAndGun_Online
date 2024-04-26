using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectGunsByOpponent : InstantEffect
{
    public SelectGuns.PurposeOfGun purposeOfGun;
    public int numberOfGuns;
    public bool isRandom;
    public SelectGuns.GunSearchCondition condition;
    public ValuesToReferTo returnResultTo;
}
