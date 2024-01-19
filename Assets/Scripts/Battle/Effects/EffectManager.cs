using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public FieldManager FM;
    
    /// <summary>
    /// ���ݔ������̌��ʈꗗ
    /// </summary>
    // �����I�ɂ́A���ʏ����͂��̃��X�g�ɒǉ����Ă���s�����̃��X�g����̍폜���ȂĊ�������悤�ɂ�����
    public List<InstantEffect> ongoingEffects;

    /// <summary>
    /// Active�ɂȂ��Ă�����ʂ̈ꗗ�i���܂��̏u�Ԃ͌��ʂ��������Ă��Ȃ����̂��܂ށj
    /// </summary>
    public List<ContinuousEffect> activeEffects;

    /// <summary>
    /// ���ݔ����ҋ@���̌��ʈꗗ�i�ʃJ�[�h�̔������ɗU�������J�[�h����荞�ޗp�j
    /// </summary>
    public List<Effect> triggeredEffects;

    public List<EffectHub> reactionsSet;

    public List<EffectHub> specialBulletsLoadedToRightGun;
    public List<EffectHub> specialBulletsLoadedToLeftGun;

    public void UnpackEffectHub(EffectHub currentHub)
    {
        foreach (Effect effect in currentHub.effects) 
        { 

        }
    }

    public void ResolveEffects(Effect.EventCue currentCue)
    {

    }

    public void RecieveCue(Effect.EventCue currentCue)
    {
        activeEffects.FindAll(item => item.cue == currentCue); 
       
    }

    public void RecieveCue(Effect.EventCue currentCue, FiredProjectile firedProjectile) 
    {
        if(currentCue == Effect.EventCue.UponPlayerGunFire)
        {
            PlayerGunFireSequence(firedProjectile);
        }
    }

    public void PlayerGunFireSequence(FiredProjectile firedProjectile) 
    {
        
    }
}
