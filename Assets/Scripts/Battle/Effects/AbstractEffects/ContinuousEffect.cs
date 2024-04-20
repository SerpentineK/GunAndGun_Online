using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ContinuousEffect�A�܂�p��������ʂ̃N���X�B
// ����������Effect��InstantEffect�ł���ActivateContinuous�̏���/�����ɂ��EffectManager.cs��activeEffects�ɓo�^����A
// �K������EventCue��Manager�ɑ���ꂽ���ɌĂяo������ʂ𔭓�����B
public class ContinuousEffect : Effect
{
    // ���ʂ���������^�C�~���O���L�^���邽�߂�EventCue�^�̕ϐ�
    public EventCue cue = EventCue.Immediate;

    /// <summary>
    /// ����EventCue�������ʓ��m���d�Ȃ����ꍇ�̏����̗D��x�B
    /// Constant�����͑S�Ă�Cue��ʂ��Đ����������邽�߁A0��Constant��EventCue�Ɏ���Effect���u����Ă���B
    /// Constant��EventCue�Ɏ���Effect��priority3�́y�s���z�Ȃǂɂ�薳�������ꂽ�ꍇ�A
    /// ���̏�Ŗ��������ꂽEffect���u�������̌��ʁv���X�g�iEffectManager.cs�Q�Ɓj���珜�O����Constant��Effect���Ăя�������B
    /// �iConstant�͏�ɔ����������Ă��邽�߁A�������͂��܂�֌W�Ȃ��C������j
    /// 
    /// 0: Constant��EventCue�Ɏ���Effect
    /// 1: ���[������(�^�[���J�n����BURN�J�E���^�[�ɂ��_���[�W������@�e�ɂ��ˌ��̊�b�_���[�W�v�Z�Ȃ�)
    /// 2: Constant�̂ق���EventCue�����i������
    /// 3: �u�ˌ����v���ʂ́y�e�e�z���D����g�p���ꂽ�y�s���z�ȂǁA�i�����ʂ������čł��D��x�̍������ʁi�y�s���z�ɂ́y�Ή��z�𔭓��ł��Ȃ��j
    /// 4: �ˌ��A�y�Z�\�z�ɑ΂���y�Ή��z
    /// 5: HIT�u�����ʁi�ˌ��������O�ɔ�����錾�ł������ȃJ�[�h�A�s���d�͌����t�Ȃǁj
    /// 6: �ˌ��A�y�Z�\�z�̍ŏI����(�ˌ��ɔ����@�e�̓���\�͂̉������܂�)
    /// 7: �u�ˌ������ꍇ�v���ʂ́y�e�e�z�ȂǁA����̌��ʂ̂��Ƃɔ����������
    /// 
    /// �Ȃ��A�D��x�������ł���ꍇ�^�[���v���C���[�̌��ʂ����ɏ�������B
    /// �܂��A����v���C���[���ێ�������ʂ̏����D��x���d�Ȃ����ꍇ�͍D���ȏ��Ԃŏ������Ă悢�B
    /// </summary>
    public int priority = 3;

    // �����錾���Ɍ��ʂ��������邩�ۂ�
    // (�s�s���s���t��s�C���t�A�s�O�b�֒e�t�ȂǁA����������ł����������Ȃ����ʂ�z��)
    [HideInInspector]
    public bool willOperateOnActivation = true;
}
