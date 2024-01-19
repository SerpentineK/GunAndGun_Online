using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableFire : AbstractEffect_Disable
{
    public enum BypassCondition
    {
        None,
        MoreThanThreeBullets
    }

    public BypassCondition bypassCondition;

    [HideInInspector]
    public Gun[] disabledGuns;
}
