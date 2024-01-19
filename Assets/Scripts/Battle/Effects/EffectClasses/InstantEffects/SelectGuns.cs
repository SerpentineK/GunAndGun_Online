using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectGuns : AbstractEffect_Select
{
    public enum PurposeOfGun
    {
        None,
        Reload,
        DisableReload,
        DisableFire,
        Nullify,
        Overclock,
        TargetOfEffect
    }
    public enum GunSearchCondition
    {
        None,
        HasntFiredThisTurn,
        AbleToReload,
        AbleToReloadManually,
        AbleToReloadByEffect,
        AbleToReloadNormalBullets,
        AbleToReloadSpecialBullets,
        IsNotSelectedByFormerEffect
    }
    public PurposeOfGun purposeOfGun;
    public int numberOfGuns;
    public GunSearchCondition condition;
}
