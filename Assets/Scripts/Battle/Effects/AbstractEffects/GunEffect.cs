using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEffect : Effect
{
    public enum EffectClassification
    {
        Fire,
        SideEffect,
        Passive
    }

    public EffectClassification classification;

    // このGunEffectを発動するのにどれだけの銃弾が必要か（射撃アクション用の変数）
    public int bulletsRequired;

    // このGunEffectがどれだけのHITを与えるか（射撃アクション用の変数）
    public int hitToApply;

    // このGunEffectが永続効果か否か（ドドメの【銃弾】装填不可を想定）
    public bool isEternal;

    
}
