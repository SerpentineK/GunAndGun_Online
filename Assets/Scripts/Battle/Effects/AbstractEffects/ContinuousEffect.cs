using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ContinuousEffect、つまり継続する効果のクラス。
// こういったEffectはInstantEffectであるActivateContinuousの処理/解決によりEffectManager.csのactiveEffectsに登録され、
// 適合するEventCueがManagerに送られた時に呼び出され効果を発動する。
public class ContinuousEffect : Effect
{
    // 効果が発生するタイミングを記録するためのEventCue型の変数
    public EventCue cue = EventCue.Immediate;

    /// <summary>
    /// 同じEventCueを持つ効果同士が重なった場合の処理の優先度。
    /// Constantだけは全てのCueを通して成立し続けるため、0にConstantをEventCueに持つEffectが置かれている。
    /// ConstantをEventCueに持つEffectがpriority3の【行動】などにより無効化された場合、
    /// その場で無効化されたEffectを「発動中の効果」リスト（EffectManager.cs参照）から除外してConstantのEffectを再び処理する。
    /// （Constantは常に発動し続けているため、処理順はあまり関係ない気がする）
    /// 
    /// 0: ConstantをEventCueに持つEffect
    /// 1: ルール効果(ターン開始時のBURNカウンターによるダメージ処理や機銃による射撃の基礎ダメージ計算など)
    /// 2: ConstantのほかにEventCueを持つ永続効果
    /// 3: 「射撃時」効果の【銃弾】や手札から使用された【行動】など、永続効果を除いて最も優先度の高い効果（【行動】には【対応】を発動できない）
    /// 4: 射撃、【技能】に対する【対応】
    /// 5: HIT置換効果（射撃解決直前に発動を宣言できる特殊なカード、《多重力光球》など）
    /// 6: 射撃、【技能】の最終解決(射撃に伴う機銃の特殊能力の解決を含む)
    /// 7: 「射撃した場合」効果の【銃弾】など、特定の効果のあとに発生する効果
    /// 
    /// なお、優先度が同じである場合ターンプレイヤーの効果から先に処理する。
    /// また、同一プレイヤーが保持する効果の処理優先度が重なった場合は好きな順番で処理してよい。
    /// </summary>
    public int priority = 3;

    // 発動宣言時に効果が発動するか否か
    // (《不撓不屈》や《気迫》、《徹甲榴弾》など、特定条件下でしか発動しない効果を想定)
    [HideInInspector]
    public bool willOperateOnActivation = true;
}
