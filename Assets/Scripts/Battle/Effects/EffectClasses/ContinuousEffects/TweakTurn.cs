using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �s�z���t��s�u���b�c�t�ȂǁA�^�[���֘A�̏���
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
