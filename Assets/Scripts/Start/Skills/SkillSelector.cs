using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSelector : MonoBehaviour
{
    public GunnerData selectedGunnerData;
    public SkillCandidate[] candidateArray;
    public SkillCandidate activeCandidate;

    public void OrganizeSkills()
    {
        SkillData[] skillArray = selectedGunnerData.GetSkillArray();
        if (skillArray.Length == candidateArray.Length)
        {
            for (int i = 0; i < skillArray.Length; i++)
            {
                candidateArray[i].data = skillArray[i];
                candidateArray[i].SetupSkill();
            }
            candidateArray[0].ActivateTarget();
            activeCandidate = candidateArray[0];
        }
    }
}
