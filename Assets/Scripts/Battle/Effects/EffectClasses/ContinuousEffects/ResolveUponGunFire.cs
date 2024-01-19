using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolveUponGunFire : ContinuousEffect
{
    [HideInInspector] public Gun gun;
    public List<Effect> effects;
    public List<Effect> extraEffects;
}
