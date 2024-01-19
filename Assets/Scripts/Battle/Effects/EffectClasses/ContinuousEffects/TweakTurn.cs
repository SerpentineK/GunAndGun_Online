using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 《陽炎》や《ブリッツ》など、ターン関連の処理
public class TweakTurn : ContinuousEffect
{
    public enum EffectToTurn
    {
        None,
        Replay,
        MakeOpponentSkip
    }
    public EffectToTurn effectToTurn;
}
