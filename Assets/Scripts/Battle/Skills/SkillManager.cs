using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public Skill playerSkill;
    public Skill opponentSkill;

    public void Awake()
    {
        instance = this;
    }
}
