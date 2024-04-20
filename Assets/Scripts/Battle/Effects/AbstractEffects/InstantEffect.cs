using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// InstantEffect、つまり即座に解決される効果のクラス。
// InstantEffectは親EffectHubに紐づいた効果の行使時にEffectManager.csのOngoingEffectsに登録し、処理後はそこから削除する。
// こういったEffectはEventCueを設定する必要がない(呼び出されるのは必ず何らかのEffectHubの一部としてなので)。
public class InstantEffect : Effect
{
    // 発動宣言時に効果が発動するか否か
    // (《不撓不屈》や《気迫》、《徹甲榴弾》など、特定条件下でしか発動しない効果を想定)
    [HideInInspector]
    public bool willOperateOnActivation = true;

    // 《起死回生》や技能のBURSTなど、テキストの置き換えが起こるか否か
    [HideInInspector]
    public bool willOperateAsAlternative = false;

    // テキスト置換発生時の変更先効果
    public Effect altEffect;

    // テキスト置換が発生する条件
    public AltEffectCondition altEffectCondition;

    // 条件判定の際に参照する値
    public ValuesToReferTo altActivationRefersToValue;

}
