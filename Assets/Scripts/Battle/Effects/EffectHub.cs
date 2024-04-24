using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �f�b�L�J�[�h���ʂ�e�m���ʂȂǁA�u��Z�߂ɂȂ������ʁv�̃N���X�B
// Effect�ł͂Ȃ�ContinuousEffect���p�����Ă���̂́AHub��ContinuousEffect�Ƃ��Ĉ��������ꍇ�����݂��邽�߁B
// (�e�m���ʂ͌p�����ʂȂ̂�Continuous�Ƃ���activeEffects�ɓo�^���Ă����������A���e�����G�ŏ������ʓ|...�Ȃ�)
// �y�s���z�J�[�h�g�p�����ɂƂ�ƁAEffectHub�̌��ʉ������@��

// 1: PlayerManager�����肪�J�[�h�ɕR�Â���EffectHub(�y�s���z�Ȃ̂�cue��Immediate)��activeEffects�ɓo�^����
// 2: ������EffectHub��o�^�������炩��Manager��EffectManager��Immediate��EventCue�𑗂�(EffectManager.cs��RecieveCue()�֐�)
// 3: EffectManager��activeEffects�����������Acue��Immediate�̂��̂�����Ă���
// 4: EffectManager��EffectHub���̏��������s����
// 5: EffectManager�������̏I������EffectHub��activeEffects����폜����

// �ƂȂ�B
// �y�Ή��z�Ȃ�1��reactionsSet�ւ̓o�^�A2������̎ˌ���/�Z�\�g�p���uUponOpponentGunFire�v��EventCue���M�ɕω�����B
// �y�@�\�z�Ȃ�cue���ʂɐݒ肵�Ă��邽�ߔ������_��Hub��ContinuousEffect�Ƃ��Ĉ����AactiveEffects�ɓo�^����B
// �y�e�e�z�Ȃ瑕�U����specialBulletsLoaded�ɓo�^���ACue��������i���������u���̃J�[�h�����U����Ă���@�e�̎ˌ��v���g���K�[�j�����B
[Serializable]
[CreateAssetMenu(fileName = "EffectHub", menuName = "CreateEffectHub")]
public class EffectHub : ContinuousEffect
{
    [HideInInspector]
    // Hub�����ѕt���Ă���J�[�h�{��
    public Card thisCard;

    [HideInInspector]
    // �������@�e�{��
    public Gun thisGun;

    [HideInInspector]
    // �e�m�{��
    public Gunner thisGunner;

    [HideInInspector]
    // �Z�\�{��
    public Skill thisSkill;

    // �i�[���Ă���Effect�̃��X�g
    public List<Effect> effects;

    // �J�[�h���g�킸�Ƃ������I�ɔ���������ʂ�Hub
    // (�s�I�[�o�[�q�[�g�t�̑��^�[���g�p�s�Ȃ�)
    public ContinuousEffect automaticEffect;

    // ��̎������ʂ������������
    public EndContinuous automaticEndEffect;

    // Hub���R�Â��Ă���J�[�h/�e�m/�@�e/�Z�\
    public ScriptableObject attachedData;

    // InstantEffect�ł͂Ȃ����ߒ��ڔ����͂����Ȃ����A�ւ��̂������
    public List<Effect> ExtraEffects;

    // �J�[�h�g�p��1�^�[��1��̐������������Ă��邩�ۂ���bool�l
    public bool usageIsOncePerTurn = false;

    // ���ʔ�����1�^�[��1��̐������������Ă��邩�ۂ���bool�l
    // �オfalse�ł��ꂪtrue�̏ꍇ�A�J�[�h�g�p�͂ł��邪���ʂ͔������Ȃ�(�s�|�C�Y���o���b�g�t�Ȃ�)
    public bool activationIsOncePerTurn = false;

    // �q�J�M���́y���~�b�g�z�p��bool�l
    public bool isLimitedByHikagiri = false;

    // ��D�U�����邩�ۂ���bool�l(����͎̂�����`���[�j���O�E�{������)
    public bool effectTriggersFromHand = false;

    // �I�[�o�[�N���b�N�J�[�h���ۂ�
    public bool isOverclock = false;

    // ����J�[�h���Łu�Ώۂ�I�сA�Ώۂɂ��Ă̂݌��ʔ����v�Ƃ����e�L�X�g������ꍇ�A
    // ����̎����ł͑ΏۑI���ƌ��ʔ����͕ʁX��Effect�Ȃ̂�Effect����ʂ̃I�u�W�F�N�g��
    // �󂯓n���p�̃��X�g��p�ӂ���K�v������B

    // �v���C���[��
    [HideInInspector]
    public Player[] playerResults;

    // �qEffect���������ɔ������邩(Effect�Ŕ����̗L���𔻒肵�A���̒l�������ɕԂ��A���̌㎟��Effect�����̒l���Q�Ƃ���)
    [HideInInspector]
    public bool childrenOperateOnActivation = true;

    // �e�L�X�g�����������s����(Effect�Ńe�L�X�g���������̗L���𔻒肵�A���̒l�������ɕԂ��A���̌㎟��Effect�����̒l���Q�Ƃ���)
    [HideInInspector]
    public bool childrenOperateAsAltEffect;

    // Effect��
    [HideInInspector]
    public Effect[] effectResults;


    // �J�[�h��
    [HideInInspector]
    public Card[] cardResults01;
    [HideInInspector]
    public Card[] cardResults02;
    [HideInInspector]
    public Card[] cardResults03;
    [HideInInspector]
    public Card[] cardResults04;
    [HideInInspector]
    public Card[] cardResults05;
    [HideInInspector]
    public Card[] cardResults06;

    // �@�e��
    [HideInInspector]
    public Gun[] gunResults01;
    [HideInInspector]
    public Gun[] gunResults02;

    // �f�b�L��
    [HideInInspector]
    public Field fieldResult01;
    [HideInInspector]
    public Field fieldResult02;
    [HideInInspector]
    public Field fieldResult03;

    [HideInInspector]
    public Field[] fieldSetResult01;
    [HideInInspector]
    public Field[] fieldSetResult02;
    [HideInInspector]
    public Field[] fieldSetResult03;

    // ���l��
    [HideInInspector]
    public NumeralValue intResult01;
    [HideInInspector]
    public NumeralValue intResult02;

    // projectile��
    [HideInInspector]
    public FiredProjectile projectileResult;

    // EffectHub��
    [HideInInspector]
    public EffectHub hubResult01;
    [HideInInspector]
    public EffectHub hubResult02;

    // Data��
    [HideInInspector]
    public ScriptableObject dataResult01;
    [HideInInspector]
    public ScriptableObject dataResult02;

    //Skill��
    [HideInInspector]
    public Skill skillResult;

    // �J�[�h��ޕ�
    [HideInInspector]
    public CardData.CardType cardTypeResult = CardData.CardType.Other;

    // (�J���p)�L���ς݂��ۂ�
    public bool finishedInput;
}
