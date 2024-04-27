using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Effect;

public class ApplyShock : InstantEffect
{
    public ApplyHit.MeOrYou whoToApply;
    public int numberOfShock;
    public ValuesToReferTo returnResultToValue;

    private Player victim;

    public override void Resolve()
    {
        if (whoToApply == ApplyHit.MeOrYou.Player)
        {
            victim = EffectManager.instance.myself;
        }
        else
        {
            victim = EffectManager.instance.opponent;
        }

        // numberOfShockと現状のshockCountersの和が0以上であることを確かめる。
        // （当然ながらショックカウンターを取り除く『シャットダウン』以外ではこの条件は必ず満たされる）
        if (victim.shockCounters + numberOfShock >= 0) 
        {
            victim.shockCounters += numberOfShock;
        }
        // 0以下になる場合、つまりnumberOfShockが負の数で絶対値において現状のshockCounters以上である場合。
        // ガンナガンは「完遂できない場合できる限り行う」というルールがあるっぽいので、ショックカウンターを0にする。
        else
        {
            victim.shockCounters = 0;
        }
    }
}
