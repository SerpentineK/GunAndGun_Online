using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyHit : InstantEffect
{
    public enum MeOrYou
    {
        None,
        Player,
        Opponent
    }

    public MeOrYou whoToApplyHit;
    public bool hitRefersToHub;
    public ValuesToReferTo valueToReferTo;
    public ModifyHit.MethodOfModify methodOfModify;
    public int modifyer;
    public int hitToApply;

    public override void Resolve()
    {
        Player victim = null;
        switch (whoToApplyHit) 
        {
            case MeOrYou.Player:
                victim = EffectManager.instance.myself;
                break;
            case MeOrYou.Opponent:
                victim = EffectManager.instance.opponent;
                break;
        }

        if (hitRefersToHub)
        {
            NumeralValue myValue = EffectManager.instance.HubDictionary[valueToReferTo] as NumeralValue;
            hitToApply = myValue.value;
        }

        int actualHit = hitToApply;

        switch (methodOfModify)
        {
            case ModifyHit.MethodOfModify.Add:
                actualHit += modifyer;
                break;
            case ModifyHit.MethodOfModify.Multiply:
                actualHit *= modifyer;
                break;
            case ModifyHit.MethodOfModify.Divide:
                actualHit /= modifyer;
                break;
        }

        victim.HP -= actualHit;

        if (victim == EffectManager.instance.myself && actualHit > 0)
        {
            EffectManager.instance.RecieveCue(EventCue.TakenDamage);
            EffectManager.instance.SendCue(EventCue.OpponentTakenDamage);
        }
        else if (victim == EffectManager.instance.opponent && actualHit > 0)
        {
            EffectManager.instance.RecieveCue(EventCue.GivenDamage);
            EffectManager.instance.SendCue(EventCue.TakenDamage);
        }
    }
}

