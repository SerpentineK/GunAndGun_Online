using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ApplyHeal : InstantEffect
{
    public ApplyHit.MeOrYou whoToApplyHeal;
    public bool healRefersToHub;
    public ValuesToReferTo valueToReferTo;
    public ModifyHit.MethodOfModify methodOfModify;
    public int modifyer;
    public int healToApply;

    private Player patient; 

    public override void Resolve()
    {
        if (whoToApplyHeal == ApplyHit.MeOrYou.Player) 
        { 
            patient = EffectManager.instance.myself; 
        }
        else if (whoToApplyHeal == ApplyHit.MeOrYou.Opponent) 
        { 
            patient = EffectManager.instance.opponent; 
        }

        if (healRefersToHub) 
        {
            NumeralValue myValue = EffectManager.instance.HubDictionary[valueToReferTo] as NumeralValue;
            healToApply = myValue.value;
        }

        int actualHeal = healToApply;

        if (methodOfModify == ModifyHit.MethodOfModify.Add)
        {
            actualHeal = healToApply + modifyer;
        }
        else if (methodOfModify == ModifyHit.MethodOfModify.Multiply)
        {
            actualHeal = healToApply * modifyer;
        }
        else if (methodOfModify == ModifyHit.MethodOfModify.Divide) 
        {
            actualHeal = healToApply / modifyer;
        }

        patient.HP += actualHeal;

        if (patient == EffectManager.instance.myself && actualHeal > 0)
        {
            EffectManager.instance.RecieveCue(EventCue.PlayerHeal);
            EffectManager.instance.SendCue(EventCue.OpponentHeal);
        }
        else if (patient == EffectManager.instance.opponent && actualHeal > 0)
        {
            EffectManager.instance.RecieveCue(EventCue.OpponentHeal);
            EffectManager.instance.SendCue(EventCue.PlayerHeal);
        }
    }
}
