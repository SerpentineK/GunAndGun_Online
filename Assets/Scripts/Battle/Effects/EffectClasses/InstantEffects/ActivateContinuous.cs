using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ActivateContinuous : InstantEffect
{
    public bool giveValueToContinuous;
    public ValuesToReferTo valueToGive;
    public EventCue continuousStartCue = EventCue.Immediate;
    public ContinuousEffect continuous;
    public EndContinuous endContinuous;
   
    // Hub�̒l���Q�Ƃ���target�ł���continuous�̓K�؂ȕϐ��ɑ������B
    public void InputValuesReferringToHub(ContinuousEffect target)
    {
        string effectClassName = target.GetEffectClassName();

        if (effectClassName == "DisableCardUsage") 
        {
            DisableCardUsage target2 = target as DisableCardUsage;
            target2.cardsToDisable = EffectManager.instance.HubDictionary[valueToGive] as Card[];
        }

        if (effectClassName == "DisableDrawFromDeck")
        {
            DisableDrawFromDeck target2 = target as DisableDrawFromDeck;
            target2.disabledDeck = EffectManager.instance.HubDictionary[valueToGive] as Field;
        }

        if (effectClassName == "DisableEffects")
        {
            DisableEffects target2 = target as DisableEffects;
            target2.disabledEffects = EffectManager.instance.HubDictionary[valueToGive] as Effect[];
        }

        if (effectClassName == "DisableFire") 
        {
            DisableFire target2 = target as DisableFire;
            target2.disabledGuns = EffectManager.instance.HubDictionary[valueToGive] as Gun[];
        }

        if (effectClassName == "DisableReload")
        {
            DisableReload target2 = target as DisableReload;
            target2.disabledGuns = EffectManager.instance.HubDictionary[valueToGive] as Gun[];
        }

        if (effectClassName == "DisableSkillUsage")
        {
            DisableSkillUsage target2 = target as DisableSkillUsage;
            target2.disabledSkill = EffectManager.instance.HubDictionary[valueToGive] as Skill;
        }

        if (effectClassName == "ModifyBulletCount")
        {
            ModifyBulletCount target2 = target as ModifyBulletCount;
            target2.gunToModify = EffectManager.instance.HubDictionary[valueToGive] as Gun;
        }

        if (effectClassName == "ResolveUponGunFire")
        {
            ResolveUponGunFire target2 = target as ResolveUponGunFire;
            target2.gun = EffectManager.instance.HubDictionary[valueToGive] as Gun;
        }
    }

    public override void Resolve()
    {
        // �l��n���K�v������Γn���B
        if (giveValueToContinuous) 
        {
            InputValuesReferringToHub(continuous);
        }
        // �����ɔ�������p�����ʂȂ�activeEffects�Ƀu�`���ށB
        if (continuousStartCue == EventCue.Immediate) 
        {
            EffectManager.instance.activeEffects.Add(continuous);
            EffectManager.instance.activeEffects.Add(endContinuous);
        }
        // ������(�u���̑���̃^�[���J�n���Ɍ��ʊJ�n�v�Ȃ�)�̏ꍇ��DormantContinuous�Ƃ�����p�N���X(!?)
        // �ɖڊo�߂�^�C�~���O�ƈꏏ�Ɋi�[���AdormantEffects�Ƃ����ʂ̃��X�g�Ƀu�`���ށB
        else
        {
            DormantContinuous dormant = new(continuous, continuousStartCue);
            EffectManager.instance.dormantEffects.Add(dormant);
            DormantContinuous dormant2 = new(endContinuous, continuousStartCue);
            EffectManager.instance.dormantEffects.Add(dormant2);
        }
    }
}
