using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// InstantEffect�A�܂葦���ɉ����������ʂ̃N���X�B
// InstantEffect�͐eEffectHub�ɕR�Â������ʂ̍s�g����EffectManager.cs��OngoingEffects�ɓo�^���A������͂�������폜����B
// ����������Effect��EventCue��ݒ肷��K�v���Ȃ�(�Ăяo�����͕̂K�����炩��EffectHub�̈ꕔ�Ƃ��ĂȂ̂�)�B
public class InstantEffect : Effect
{
    // �����錾���Ɍ��ʂ��������邩�ۂ�
    // (�s�s���s���t��s�C���t�A�s�O�b�֒e�t�ȂǁA����������ł����������Ȃ����ʂ�z��)
    [HideInInspector]
    public bool willOperateOnActivation = true;

    // �s�N���񐶁t��Z�\��BURST�ȂǁA�e�L�X�g�̒u���������N���邩�ۂ�
    [HideInInspector]
    public bool willOperateAsAlternative = false;

    // �e�L�X�g�u���������̕ύX�����
    public Effect altEffect;

    // �e�L�X�g�u���������������
    public AltEffectCondition altEffectCondition;

    // ��������̍ۂɎQ�Ƃ���l
    public ValuesToReferTo altActivationRefersToValue;

}
