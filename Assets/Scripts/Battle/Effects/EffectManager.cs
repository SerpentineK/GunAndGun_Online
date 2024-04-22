using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;

    public FieldManager FM;
    
    /// <summary>
    /// ���ݔ������̌��ʈꗗ
    /// </summary>
    // �����I�ɂ́A���ʏ����͂��̃��X�g�ɒǉ����Ă���s�����̃��X�g����̍폜���ȂĊ�������悤�ɂ�����
    public List<InstantEffect> ongoingEffects;

    /// <summary>
    /// Active�ɂȂ��Ă�����ʂ̈ꗗ�i���܂��̏u�Ԃ͌��ʂ��������Ă��Ȃ����̂��܂ށj�B
    /// �L�������ꂽ�y�@�\�z��e�m���ʂȂǂ͂����ɓ���B
    /// </summary>
    public List<ContinuousEffect> activeEffects;

    /// <summary>
    /// ���ݔ����ҋ@���̌��ʈꗗ�i�ʃJ�[�h�̔������ɗU�������J�[�h����荞�ޗp�j
    /// </summary>
    public List<Effect> triggeredEffects;

    public List<EffectHub> reactionsSet;
    public List<EffectHub> mechanismsActivated;

    public List<EffectHub> specialBulletsLoadedToRightGun;
    public List<EffectHub> specialBulletsLoadedToLeftGun;

    public void Awake()
    {
        instance = this;
    }

    public void UnpackEffectHub(EffectHub currentHub)
    {
        foreach (Effect effect in currentHub.effects) 
        {
            effect.Resolve();
        }
    }

    public void RecieveCue(Effect.EventCue currentCue)
    {
        triggeredEffects.AddRange(activeEffects.FindAll(item => item.cue == currentCue));
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


    public void UseAction(Card card)
    {
        UnpackEffectHub(card.cardEffectHub);
    }

    public void SetReaction(Card card)
    {
        reactionsSet.Add(card.cardEffectHub);
    }

    public void ActivateMechanism(Card card)
    {
        mechanismsActivated.Add(card.cardEffectHub);
        activeEffects.Add(card.cardEffectHub);
    }
}
