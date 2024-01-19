using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiredProjectile : Object
{
    // 相手に与えるダメージ
    public int HIT;
    // 自分に与えるダメージ
    public int RECOIL;
    // 相手のセットされているカード、装填、ボルテージ、またはコスト1以下の【機能】をこの数まで捨て札に置く
    public int BREAK;
    // 相手にバーンカウンター(ターン開始時各カウンターにつき3ダメージ)をこの数置く
    public int BURN;
    // 相手の手札からこの数捨て札に置く
    public int JOLT;
    

    public enum SideEffect 
    {
        // 相手にダメージを与えた時、その分だけ回復する
        RECOVER,
        // 相手にダメージを与えた時、その分だけ相手の山札の上から捨て札に置く
        ANNIHILATION,
        // 射撃した場合、ターン終了時に捨て札からカードを1枚手札に加える
        RETRIEVAL
    }

    public SideEffect sideEffect;
}
