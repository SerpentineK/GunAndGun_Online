using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckField : Field
{
    // マカの技能用のプロパティ。
    // trueの場合、カードを引けずもう片方のデッキが0枚になったらゲームに敗北する。
    public bool isNullifyed = false; 
}
