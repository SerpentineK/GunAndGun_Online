using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableReload : AbstractEffect_Disable
{
    public Gun[] disabledGuns;
    // 通常弾の装填を許可するか(ドドメを想定)
    public bool allowNormalBullets;
    // 特殊弾の装填を許可するか(ダイダラを想定)
    public bool allowSpecialBullets;
    // 通常アクションによる装填を許可するか（《昏倒弾》を想定）
    public bool allowNormalReload;
    // 効果による装填を許可するか(《フリーズバレット》を想定)
    public bool allowCardEffects;
}
