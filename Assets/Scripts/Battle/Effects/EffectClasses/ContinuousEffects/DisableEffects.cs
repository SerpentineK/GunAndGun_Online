using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableEffects : AbstractEffect_Disable
{
    // 無効化から除外したいEffectはdisabledEffectsに入れない
    public Effect[] disabledEffects;
}
