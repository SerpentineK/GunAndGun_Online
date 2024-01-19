using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverSkill : InstantEffect
{
    public enum RecoverCandidate
    {
        None,
        Player,
        Opponent
    }
    public RecoverCandidate whoseSkillToRecover;
}
