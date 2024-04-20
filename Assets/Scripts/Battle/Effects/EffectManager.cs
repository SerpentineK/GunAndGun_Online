using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;

    public FieldManager FM;
    
    /// <summary>
    /// 現在発動中の効果一覧
    /// </summary>
    // 将来的には、効果処理はこのリストに追加してから行いこのリストからの削除を以て完了するようにしたい
    public List<InstantEffect> ongoingEffects;

    /// <summary>
    /// Activeになっている効果の一覧（いまこの瞬間は効果を処理していないものも含む）。
    /// 有効化された【機能】や銃士効果などはここに入る。
    /// </summary>
    public List<ContinuousEffect> activeEffects;

    /// <summary>
    /// 現在発動待機中の効果一覧（別カードの発動中に誘発したカードを放り込む用）
    /// </summary>
    public List<Effect> triggeredEffects;

    public List<EffectHub> reactionsSet;

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

        }
    }

    public void ResolveEffects(Effect.EventCue currentCue)
    {

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
   
    public void InitiateEffectSequence(EffectHub effectHub)
    {

    }

    public void UseCard(Card card)
    {
        Card.CardStatus status = card.ExamineBeforePlay();
        if (status == Card.CardStatus.PLAYABLE)
        {
            if (card.cardType == CardData.CardType.Action)
            {

            }
        }
    }

    public void SetReaction(Card card)
    {

    }
}
