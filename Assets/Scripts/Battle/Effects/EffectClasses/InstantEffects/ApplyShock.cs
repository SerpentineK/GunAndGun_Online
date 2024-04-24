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

        // �q�_���̃I�[�o�[�N���b�N�̓V���b�N�J�E���^�[��3��菜�����ꍇ�̂ݔ�������B
        // �܂�numberOfShock�ƌ����shockCounters�̘a��0�ȏ�ł��邱�Ƃ��m���߂�B
        // �i���R�Ȃ���V���b�N�J�E���^�[����菜���w�V���b�g�_�E���x�ȊO�ł͂��̏����͕K�����������j
        if (victim.shockCounters + numberOfShock >= 0) 
        {
            victim.shockCounters += numberOfShock;
            if (returnResultToValue != ValuesToReferTo.None)
            {
                EffectManager.instance.HubDictionary[returnResultToValue] = true;
            }
        }
        // 0�ȉ��ɂȂ�ꍇ�A�܂�numberOfShock�����̐��Ő�Βl�ɂ����Č����shockCounters�ȏ�ł���ꍇ�B
        // �K���i�K���́u�����ł��Ȃ��ꍇ�ł������s���v�Ƃ������[����������ۂ��̂ŁA�V���b�N�J�E���^�[��0�ɂ���Hub�̉������~�߂�B
        else
        {
            victim.shockCounters = 0; 
            EffectManager.instance.HubDictionary[returnResultToValue] = false;
        }
    }
}
