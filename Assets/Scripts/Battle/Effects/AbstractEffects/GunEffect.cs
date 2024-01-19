using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEffect : Effect
{
    public enum EffectClassification
    {
        Fire,
        SideEffect,
        Passive
    }

    public EffectClassification classification;

    // ����GunEffect�𔭓�����̂ɂǂꂾ���̏e�e���K�v���i�ˌ��A�N�V�����p�̕ϐ��j
    public int bulletsRequired;

    // ����GunEffect���ǂꂾ����HIT��^���邩�i�ˌ��A�N�V�����p�̕ϐ��j
    public int hitToApply;

    // ����GunEffect���i�����ʂ��ۂ��i�h�h���́y�e�e�z���U�s��z��j
    public bool isEternal;

    
}
