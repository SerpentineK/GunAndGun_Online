using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyDamageByFire : ContinuousEffect
{
    public enum ActiveCondition
    {
        None,
        Always,
        WhenPlayerHasMoreVoltThanOpponent
    }
    public enum WhoseFireToModify
    {
        None,
        Player,
        Opponent
    }
    public WhoseFireToModify whose = WhoseFireToModify.Player;
    public ActiveCondition activeCondition = ActiveCondition.Always;
    public ModifyHit.MethodOfModify methodOfModify;
    public int modifyer;
}
