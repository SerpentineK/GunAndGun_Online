using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

[Serializable]
public class Effect : ScriptableObject
{
    // [HideInInspector] public FieldManager FM;
    // [HideInInspector] public EffectManager EM;
    // [HideInInspector] public PlayerManager PM;
    // [HideInInspector] public GunManager RightGM;
    // [HideInInspector] public GunManager LeftGM;
    // [HideInInspector] public Skill skill;


    // ���ʓ��e��string
    [TextArea(3, 10)]
    public string effectContentText;

    /// <summary>
    /// ���ʔ����̃g���K�[�ƂȂ錻��enum
    /// 
    /// ��{�I�Ɍ����ʂ�̈Ӗ��B
    /// PlayerCommand�́u�v���C���[���]�ނȂ玩�^�[�������ł������ł�����ʁi�R�X�g���x�����鎞�́y�s���z��q�o�i�̏e�m���ʂȂǁj�v���w���B
    /// 
    /// �s�o���b�g�L���b�`�t�A�s�L�~�̂��߂Ȃ玀�˂�t�Ȃǂ̌p�����ʂɂ��Ắi�O�҂�UponOpponentGunFire�A��҂�TakenDamage�Ƃ�����Ɂj�ʂɃg���K�[��ݒ肵�A
    /// Constant�͏펞�v���C���[�X�e�[�^�X�ɉe������������ʁi�s�_�u���o���b�g�t��s�p�����C�Y�o���b�g�t�A�}�J�̏e�m���ʂȂǁj�Ɍ��肵�����B
    /// 
    /// �Ȃ��A�ˌ���HIT�ւ̏C���́u���łɔ������Ă���i�����ʁi�o���b�g�L���b�`�Ȃǁj������e�e���Ή��J�[�h�v�Ƃ������ԂŔ�������炵���B
    /// (�o�T�Fhttps://w.atwiki.jp/ganandganinformal/pages/23.html)
    /// ����EventCue��L������ʂ̒��ł������������݂��邱�Ƃɒ��ӁB
    /// </summary>
    public enum EventCue
    {
        // ��ɃX�e�[�^�X�ɉe����^�������
        Constant,

        // �^�[���v���C���[���C�ӂŔ����ł������
        PlayerCommand,

        // �J�[�h�g�p������
        // (���i���ʔ����ɂ��ă^�C�~���O�̎w�肪�Ȃ��J�[�h��EffectHub��AEffectHub�ɓ��������Effect�͂��ׂĂ���)
        // �Ή��ɂ��Ă�(UponPlayerGunFire�ł͂Ȃ�)����B
        Immediate,

        // �O�̌��ʂɈ������������ɔ����������
        Successive,

        // �u���U���v����
        PlayerReload,
        OpponentReload,

        // �u�ˌ����v����
        UponPlayerGunFire,
        UponOpponentGunFire,

        // �u�ˌ������ꍇ�v����
        AfterPlayerGunFire,
        AfterOpponentGunFire,

        // �u�ˌ���HIT��^�����ꍇ�v����
        GivenDamageByThisGunsFire,

        // �y�Z�\�z�g�p������
        // �i�y�Ή��z�Ɓy�Z�\�z��z��j
        PlayerSkillUsage,
        OpponentSkillUsage,

        // �u���̃^�[���v����
        // �i�u�����̃^�[����������x�s���v�Ƃ������ʂ̃J�[�h�����݂��邽�߁A���̃^�[��������̃^�[���ł���Ƃ͌���Ȃ��j
        NextTurnStart,
        NextTurnEnd,

        // �u�����̃^�[���J�n��/�I�����v����
        PlayerTurnStart,
        OpponentTurnStart,
        PlayerTurnEnd,
        OpponentTurnEnd,

        // �u�J�[�h���������Ƃ��v����
        PlayerDraw,
        OpponentDraw,

        // �uHIT��^����/�^����ꂽ�ꍇ�v����
        GivenDamage,
        TakenDamage,

        // �Q�[���J�n������
        StartedGame,

        // �i�����ʂ�endCue�p
        Never,

        // ���̃^�[���̏I����
        ThisTurnEnd,

        // ���̃J�[�h�𑕓U�����@�e�̎ˌ���
        ThisCardsLoadedGunFire,

        // ���̋@�e�̎ˌ���
        ThisGunsFire,

        // ���ݐi�s���̏������S�Ċ������A�j���[�g�����ȏ�Ԃɖ߂����Ƃ�(����̋@�e�ˌ����̂ݑΉ��������A�Ȃǂ�End)
        EndOfCurrentSequence,

        // ���肪�񕜂�����
        OpponentHeal,

        // ����̋@�e���������ɂȂ�����
        OpponentOverclock,

        // ���̃v���C���[�^�[���I����
        NextPlayerTurnEnd,

        // ���肪�u�_���[�W��^����ꂽ�Ƃ��v
        OpponentTakenDamage,

        // ���̃J�[�h���̂ĎD�ɒu���ꂽ�Ƃ�
        ThisCardIsDiscarded,

        // ���̑���̃^�[���I����
        NextOpponentTurnEnd,

        // ���̃J�[�h���{���e�[�W�ɒu���ꂽ�Ƃ�
        ThisCardIsStoredInVoltage,

        // ���̃^�[���̃��C���t�F�C�Y�I����
        ThisTurnsMainPhaseEnd,

        // �v���C���[�i�����j�̉�
        PlayerHeal,

        // ���肪�J�[�h�̌��ʂő��U���s�����Ƃ�
        OpponentReloadByEffect,

        // ���肪�e�m���ʈȊO�ŉ񕜂��s�����Ƃ�
        OpponentHealByEffect,

        // ���肪�e�m���ʈȊO�ŃJ�[�h���������Ƃ�
        OpponentDrawByEffect,
    }

    // ���ʑΏۂ̑I����enum
    public enum EffectTarget
    {
        none,

        // ���̃J�[�h
        ThisCard,

        // ���̃J�[�h�̕R�Â��Ă���@�e
        ThisCardsGun,

        // HP
        PlayerHP,
        OpponentHP,

        // ��D
        PlayerHand,
        OpponentHand,
        
        // ���U���ꂽ�J�[�h�S��
        PlayerBullets,
        OpponentBullets,

        // �R�D
        PlayerDecks,
        OpponentDecks,

        // �̂ĎD
        PlayerDiscard,
        OpponentDiscard,

        // �{���e�[�W
        PlayerVolt,
        OpponentVolt,

        // �@�\
        PlayerMech,
        OpponentMech,

        // �����D(�Ή�)
        PlayerSet,
        OpponentSet,

        // �@�e
        PlayerRightGun,
        PlayerLeftGun,
        OpponentRightGun,
        OpponentLeftGun,

        // �Z�\
        PlayerSkill,
        OpponentSkill,

        // �e�m�̑��x
        PlayerGunnerAgility,
        OpponentGunnerAgility,

        // �i�s���̏����t�F�[�Y���̂���
        CurrentSequence,

        // �i�s���̏����t�F�[�Y�ɂ����āA�����I����Ώۂɗ^���邱�ƂɌ����_�łȂ��Ă���HIT
        // �Ⴆ�΁A���̑Ώۂ�������s�����t�������𖞂����Ĕ����������HIT�C�������邩������Ȃ�
        CurrentSequenceHitToPlayer,
        CurrentSequenceHitToOpponent,

        // ���̃J�[�h�Ɠ������O�̃J�[�h
        OtherCardsWithSameName,

        // ���̃J�[�h�̋A������@�e�̑��U
        ThisCardsGunsBullets,

        // ���̃J�[�h�̋A������@�e�̃f�b�L
        ThisCardsGunsDeck,

        // �e�m�̎�D����
        PlayerGunnerHand,
        OpponentGunnerHand,

        // �S�Ă�Field
        PlayerAllFields,
        OpponentAllFields,

        // ���̃J�[�h�����U����Ă�@�e
        ThisCardsLoadedGun,

        // Select����EffectHub�ɕۊǂ���Ă���l
        SelectedInt,

        // Select���ꂽGun��Deck
        SelectedGunsDeck,

        // ���E�̋@�e�̑��U
        PlayerRightGunsBullets,
        PlayerLeftGunsBullets,
        OpponentRightGunsBullets,
        OpponentLeftGunsBullets,

        // ���̃J�[�h�����U����Ă���@�e�̑��U
        ThisCardsLoadedMagazine,

        // ���ݎˌ�����Ă���@�e
        CurrentlyFiredGun,

        // ���ݎ��s����Ă������
        CurrentlyResolvingEffect,

        // ���̋@�e(����т��̃p�����[�^)
        ThisGun,
        ThisGunsLoadedBullets,

        // �ŏI�I�ɗ^����/�^����ꂽ�_���[�W
        DamageGiven,
        DamageTaken,

        // SHOCK�J�E���^�[�̐�
        ShockOnOpponent,

        // ����ɂ��񕜂̗�
        HealByOpponent,

        // �ȑO�̌��ʂőI�������J�[�h
        SelectedCardSet01,

        // �e�f�b�L(���v���C���[�̗����̃f�b�L�����ꂼ��w�肵�����ꍇ�Ɏg��)
        PlayerRightDeck,
        PlayerLeftDeck,
        OpponentRightDeck,
        OpponentLeftDeck,

        // ���̃J�[�h�������O�ɑ����Ă���Field
        ThisCardsFormerField,

        // �X�L���̃R�X�g
        PlayerSkillCost,
        OpponentSkillCost,

        // �I�����ꂽ�@�e�̑��U
        SelectedGunsBullets,

        // �Ή��𔭓������Ώۂł���ˌ��܂��͋Z�\
        ReactedProjectile,
        ReactedSkill,

        // ���̃J�[�h��������Field
        ThisCardsField
    }

    // �J�[�h���������Č��ʂ̈ꗗ��\������Ƃ��A�ǂ̂悤�ȏ����Ō������邩enum
    public enum SearchCondition
    {
        None,
        All,
        AllSameNameCards,
        NotSameNameCard,
        IsReaction,
        IsNotReaction,
        IsDrawCard,
        IsAction,
        IsSpecialBullet,
        IsLoadedAsSpecial,
        IsMechanismWithCostUnder01,
        IsUnique,
        IsNot_ApplyHit
    }

    // �l�������I�Ȗ����Ƃ��Ď����ʂ̂Ƃ��AHub�̂ǂ̒l���Q�Ƃ��邩��enum
    public enum ValuesToReferTo
    {
        None,
        CardSet01,
        CardSet02,
        CardSet03,
        CardSet04,
        CardSet05,
        CardSet06,
        EffectSet01,
        EffectSet02,
        EffectSet03,
        GunSet01,
        GunSet02,
        Field01,
        Field02,
        Field03,
        NumeralValue01,
        NumeralValue02,
        OperateOnActivationBool,
        OperateAsAltEffectBool,
        Projectile,
        EffectHub01,
        EffectHub02,
        Data01,
        Data02,
        ThisHub,
        PlayerSet,
        FieldSet01,
        FieldSet02,
        FieldSet03,
        Skill,
        CardType
    }

    // AltEffect�ɕ��򂷂����enum
    public enum AltEffectCondition
    {
        // ����Ȃ�
        none,
        // �f�b�L�I����
        DeckIsSelected,
        // �ȑO�̌��ʂőI�����ꂽ�J�[�h���e�e�ł���
        CardIsSpecialBullet,
        // �ȑO�̌��ʂőI�����ꂽInt��0�ł���
        IntEqualsZero,
        // BURST����
        HitpointIs15OrLess,
        // �ȑO�̌��ʂőI�����ꂽ�J�[�h���s���ł���
        CardIsAction,
        // �ȑO�̌��ʂ�OperateAsAltBool�ɕԂ��ꂽ���肪true�ł���
        OperateAsAltBoolIsTrue,
    }

    // ���ʂ̔��������f�b�L�J�[�h�������ꍇ�A���̎�ʂ���肷�邽�߂̕ϐ�
    // (�_�^���́y�Ή��z�������Ƃ��A�s�o���[�W�t�́y�Ή��z�ȊO�������Ƃ�����̂�)
    public CardData.CardType cardType = CardData.CardType.Other;

    // ���ʔ������̎��ۂ̏���(�I�[�o�[���C�h�p)
    public virtual void Resolve() { }

    // �����̃N���X����Ԃ��֐�
    public string GetEffectClassName()
    {
        return this.GetType().Name;
    }
}

