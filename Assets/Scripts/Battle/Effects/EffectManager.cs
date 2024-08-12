using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;

    public FieldManager FM;

    public Player myself;
    public Player opponent;

    /// <summary>
    /// ���ݔ������̌��ʈꗗ
    /// </summary>
    // �����I�ɂ́A���ʏ����͂��̃��X�g�ɒǉ����Ă���s�����̃��X�g����̍폜���ȂĊ�������悤�ɂ�����
    public List<InstantEffect> ongoingEffects;

    /// <summary>
    /// Active�ɂȂ��Ă���p�����ʂ̈ꗗ�i���܂��̏u�Ԃ͌��ʂ��������Ă��Ȃ����̂��܂ށj�B
    /// �L�������ꂽ�y�@�\�z��e�m���ʂȂǂ͂����ɓ���B
    /// </summary>
    public List<ContinuousEffect> activeEffects;

    /// <summary>
    /// �L��������Ă��Ȃ��A�܂蔭����錾���ꂽ���̂̂܂����s�i�K�Ɏ����Ă��Ȃ��p�����ʂ̈ꗗ�B
    /// �Ⴆ�΁u�|�C�Y���o���b�g�v�̃h���[�s���ʂ͑���^�[���J�n���ɗL���ɂȂ邽�߁A�u�|�C�Y���o���b�g�v�̌��ʔ����i�K�ł͗L�������ꂸ�����ɓ���B
    /// </summary>
    public List<DormantContinuous> dormantEffects;

    /// <summary>
    /// ���ݔ����ҋ@���̌��ʈꗗ�i�ʃJ�[�h�̔������ɗU�������J�[�h����荞�ޗp�j
    /// </summary>
    public List<Effect> triggeredEffects;

    public List<EffectHub> reactionsSet;
    public List<EffectHub> mechanismsActivated;

    public List<EffectHub> specialBulletsLoadedToRightGun;
    public List<EffectHub> specialBulletsLoadedToLeftGun;

    public EffectHub resolvingHub = null;
    public Gun firedGun;

    // �y�Ή��z�J�[�h�ɑ��݂���A�Ή��Ώۂ��@�e�ˌ����Z�\�g�p���ɂ���ċ�����ς���J�[�h�̂��߂�enum�B
    public enum Sequences
    {
        Neutral,
        GunFire,
        SkillUsage
    }

    public Sequences currentSequence;

    /// <summary>
    /// Effect.ValuesToReferTo�Ƃ���enum��p����EffectHub�������Ă�����̂����ق������̂���͂���ƒl���A���Ă��閂�@�̎����^���X�g�B
    /// </summary>
    public Dictionary<Effect.ValuesToReferTo, object> HubDictionary { get; private set; }

    /// <summary>
    /// Effect.EffectTarget�Ƃ���enum��p���Č��ʂ�^�������Ώۂ���͂���ƑΏۂ̃C���X�^���X���A���Ă����Ղ̎����^���X�g�B
    /// </summary>
    public Dictionary<Effect.EffectTarget, object> TargetDictionary { get; private set; }


    public void Awake()
    {
        instance = this;
    }

    public void RefreshHubValueDictionary(EffectHub targetHub) 
    {
        Dictionary<Effect.ValuesToReferTo, object> myDict = new()
        {
            { Effect.ValuesToReferTo.CardSet01, targetHub.cardResults01 },
            { Effect.ValuesToReferTo.CardSet02, targetHub.cardResults02 },
            { Effect.ValuesToReferTo.CardSet03, targetHub.cardResults03 },
            { Effect.ValuesToReferTo.CardSet04, targetHub.cardResults04 },
            { Effect.ValuesToReferTo.CardSet05, targetHub.cardResults05 },
            { Effect.ValuesToReferTo.CardSet06, targetHub.cardResults06 },
            { Effect.ValuesToReferTo.EffectSet01, targetHub.effectResults },
            { Effect.ValuesToReferTo.GunSet01, targetHub.gunResults01 },
            { Effect.ValuesToReferTo.GunSet02, targetHub.gunResults02 },
            { Effect.ValuesToReferTo.Field01, targetHub.fieldResult01 },
            { Effect.ValuesToReferTo.Field02, targetHub.fieldResult02 },
            { Effect.ValuesToReferTo.Field03, targetHub.fieldResult03 },
            { Effect.ValuesToReferTo.NumeralValue01, targetHub.intResult01 },
            { Effect.ValuesToReferTo.NumeralValue02, targetHub.intResult02 },
            { Effect.ValuesToReferTo.OperateOnActivationBool, targetHub.childrenOperateOnActivation },
            { Effect.ValuesToReferTo.OperateAsAltEffectBool, targetHub.childrenOperateAsAltEffect },
            { Effect.ValuesToReferTo.Projectile, targetHub.projectileResult },
            { Effect.ValuesToReferTo.EffectHub01, targetHub.hubResult01 },
            { Effect.ValuesToReferTo.EffectHub02, targetHub.hubResult02 },
            { Effect.ValuesToReferTo.Data01, targetHub.dataResult01 },
            { Effect.ValuesToReferTo.Data02, targetHub.dataResult02 },
            { Effect.ValuesToReferTo.ThisHub, targetHub },
            { Effect.ValuesToReferTo.PlayerSet, targetHub.playerResults },
            { Effect.ValuesToReferTo.FieldSet01, targetHub.fieldSetResult01 },
            { Effect.ValuesToReferTo.FieldSet02, targetHub.fieldSetResult02 },
            { Effect.ValuesToReferTo.FieldSet03, targetHub.fieldSetResult03 },
            { Effect.ValuesToReferTo.Skill, targetHub.skillResult },
            { Effect.ValuesToReferTo.CardType, targetHub.cardTypeResult },
        };
        HubDictionary = myDict;
        return;
    }

    // resolvingHub�̕ϐ��ɒl�������邽�߂̃��\�b�h
    // �I�[�o�[���[�h�ł��낢��Ή��ł���悤�ɂ�����
    public void InputValueToHub(Effect.ValuesToReferTo referenceToInput, Object[] arrayToInput)
    {
        switch (referenceToInput)
        {
            case Effect.ValuesToReferTo.CardSet01:
                resolvingHub.cardResults01 = arrayToInput as Card[];
                break;
            case Effect.ValuesToReferTo.CardSet02:
                resolvingHub.cardResults02 = arrayToInput as Card[];
                break;
            case Effect.ValuesToReferTo.CardSet03:
                resolvingHub.cardResults03 = arrayToInput as Card[];
                break;
            case Effect.ValuesToReferTo.CardSet04:
                resolvingHub.cardResults04 = arrayToInput as Card[];
                break;
            case Effect.ValuesToReferTo.CardSet05:
                resolvingHub.cardResults05 = arrayToInput as Card[];
                break;
            case Effect.ValuesToReferTo.CardSet06:
                resolvingHub.cardResults06 = arrayToInput as Card[];
                break;
            case Effect.ValuesToReferTo.EffectSet01:
                resolvingHub.effectResults = arrayToInput as Effect[];
                break;
            case Effect.ValuesToReferTo.GunSet01:
                resolvingHub.gunResults01 = arrayToInput as Gun[];
                break;
            case Effect.ValuesToReferTo.GunSet02:
                resolvingHub.gunResults02 = arrayToInput as Gun[];
                break;
            case Effect.ValuesToReferTo.PlayerSet:
                resolvingHub.playerResults = arrayToInput as Player[];
                break;
            case Effect.ValuesToReferTo.FieldSet01:
                resolvingHub.fieldSetResult01 = arrayToInput as Field[];
                break;
            case Effect.ValuesToReferTo.FieldSet02:
                resolvingHub.fieldSetResult02 = arrayToInput as Field[];
                break;
            case Effect.ValuesToReferTo.FieldSet03:
                resolvingHub.fieldSetResult03 = arrayToInput as Field[];
                break;
            default:
                break;
        }
    }

    public void InputValueToHub(Effect.ValuesToReferTo referenceToInput, bool booleanToInput)
    {
        switch (referenceToInput)
        {
            case Effect.ValuesToReferTo.OperateOnActivationBool:
                resolvingHub.childrenOperateOnActivation = booleanToInput; 
                break;
            case Effect.ValuesToReferTo.OperateAsAltEffectBool:
                resolvingHub.childrenOperateAsAltEffect = booleanToInput;
                break; 
            default:
                break;
        }
    }

    public void InputValueToHub(Effect.ValuesToReferTo referenceToInput, CardData.CardType cardTypeToInput)
    {
        switch (referenceToInput)
        {
            case Effect.ValuesToReferTo.CardType:
                resolvingHub.cardTypeResult = cardTypeToInput;
                break;
            default:
                break;
        }
    }

    public void InputValueToHub(Effect.ValuesToReferTo referenceToInput, Object objectToInput)
    {
        switch (referenceToInput)
        {
            case Effect.ValuesToReferTo.Field01:
                resolvingHub.fieldResult01 = objectToInput as Field;
                break;
            case Effect.ValuesToReferTo.Field02:
                resolvingHub.fieldResult02 = objectToInput as Field;
                break;
            case Effect.ValuesToReferTo.Field03:
                resolvingHub.fieldResult03 = objectToInput as Field;
                break;
            case Effect.ValuesToReferTo.NumeralValue01:
                resolvingHub.intResult01 = objectToInput as NumeralValue;
                break;
            case Effect.ValuesToReferTo.NumeralValue02:
                resolvingHub.intResult02 = objectToInput as NumeralValue;
                break;
            case Effect.ValuesToReferTo.OperateOnActivationBool:
                break;
            case Effect.ValuesToReferTo.OperateAsAltEffectBool:
                break;
            case Effect.ValuesToReferTo.Projectile:
                resolvingHub.projectileResult = objectToInput as FiredProjectile;
                break;
            case Effect.ValuesToReferTo.EffectHub01:
                resolvingHub.hubResult01 = objectToInput as EffectHub;
                break;
            case Effect.ValuesToReferTo.EffectHub02:
                resolvingHub.hubResult02 = objectToInput as EffectHub;
                break;
            case Effect.ValuesToReferTo.Data01:
                resolvingHub.dataResult01 = objectToInput as ScriptableObject;
                break;
            case Effect.ValuesToReferTo.Data02:
                resolvingHub.dataResult02 = objectToInput as ScriptableObject;
                break;
            case Effect.ValuesToReferTo.Skill:
                resolvingHub.skillResult = objectToInput as Skill;
                break;
            default:
                break;
        }
    }

    public void RefreshTargetDictionary()
    {
        List<Field> playerBullets = new()
        {
            myself.FM.leftMagazine,
            myself.FM.rightMagazine
        };
        List<Field> playerDecks = new()
        {
            myself.FM.leftDeck,
            myself.FM.rightDeck
        };
        List<Field> playerAllFields = new()
        {
            myself.FM.hand,
            myself.FM.rightDeck,
            myself.FM.leftDeck,
            myself.FM.rightMagazine,
            myself.FM.leftMagazine,
            myself.FM.reaction,
            myself.FM.mechanism,
            myself.FM.discard,
            myself.FM.voltage
        };

        MagazineField loadedMagazine = resolvingHub.thisCard.currentField as MagazineField;

        Dictionary<Effect.EffectTarget, object> myDict = new()
        {
            { Effect.EffectTarget.ThisCard, resolvingHub.thisCard },
            { Effect.EffectTarget.ThisCardsGun, resolvingHub.thisCard.attachedGun },
            { Effect.EffectTarget.ThisCardsGunsBullets, resolvingHub.thisCard.attachedGun.magazineField },
            { Effect.EffectTarget.ThisCardsGunsDeck, resolvingHub.thisCard.attachedGun.deckField },
            { Effect.EffectTarget.ThisCardsField, resolvingHub.thisCard.currentField },
            { Effect.EffectTarget.ThisCardsFormerField, resolvingHub.thisCard.previousField },
            { Effect.EffectTarget.ThisCardsLoadedMagazine, loadedMagazine },
            { Effect.EffectTarget.ThisCardsLoadedGun, loadedMagazine.masterGun },
            { Effect.EffectTarget.ThisGun, resolvingHub.thisGun },
            { Effect.EffectTarget.ThisGunsLoadedBullets, resolvingHub.thisGun.magazineField },
            { Effect.EffectTarget.PlayerHP, myself.HP },
            { Effect.EffectTarget.PlayerHand, myself.FM.hand },
            { Effect.EffectTarget.PlayerBullets, playerBullets },
            { Effect.EffectTarget.PlayerDecks, playerDecks},
            { Effect.EffectTarget.PlayerDiscard, myself.FM.discard },
            { Effect.EffectTarget.PlayerVolt, myself.FM.voltage },
            { Effect.EffectTarget.PlayerMech, myself.FM.mechanism },
            { Effect.EffectTarget.PlayerSet, myself.FM.reaction },
            { Effect.EffectTarget.PlayerRightGun, myself.rightGun },
            { Effect.EffectTarget.PlayerLeftGun, myself.leftGun },
            { Effect.EffectTarget.PlayerSkill, myself.skill },
            { Effect.EffectTarget.PlayerGunnerAgility, myself.gunner.agility },
            { Effect.EffectTarget.PlayerGunnerHand, myself.gunner.hand },
            { Effect.EffectTarget.PlayerAllFields, playerAllFields },
            { Effect.EffectTarget.PlayerRightGunsBullets, myself.FM.rightMagazine },
            { Effect.EffectTarget.PlayerLeftGunsBullets, myself.FM.leftMagazine },
            { Effect.EffectTarget.PlayerSkillCost, myself.skill.cost },
            { Effect.EffectTarget.OpponentHP, opponent.HP }
        };

        TargetDictionary = myDict;
        return;
    }

    public void UnpackEffectHub(EffectHub currentHub)
    {
        resolvingHub = currentHub;
        currentHub.childrenOperateOnActivation = true;
        foreach (Effect effect in currentHub.effects) 
        {
            InstantEffect instantEffect = effect as InstantEffect;
            RefreshHubValueDictionary(currentHub);
            RefreshTargetDictionary();
            if (currentHub.childrenOperateOnActivation && !currentHub.childrenOperateAsAltEffect)
            {
                instantEffect.Resolve();
            }
            else if (currentHub.childrenOperateOnActivation && currentHub.childrenOperateAsAltEffect)
            {
                instantEffect.altEffect.Resolve();
            }
        }
    }

    public void RecieveCue(Effect.EventCue currentCue)
    {
        triggeredEffects.AddRange(activeEffects.FindAll(item => item.cue == currentCue));
    }

    public void RecieveCue(Effect.EventCue currentCue, FiredProjectile firedProjectile) 
    {
        if(currentCue == Effect.EventCue.UponPlayerGunFire)
        {
            PlayerGunFireSequence(firedProjectile);
        }
    }

    public void PlayerGunFireSequence(FiredProjectile firedProjectile) 
    {
        
    }

    public void SendCue(Effect.EventCue currentCue)
    {

    }


    public void UseAction(Card card)
    {
        UnpackEffectHub(card.cardEffectHub);
    }

    public void SetReaction(Card card)
    {
        reactionsSet.Add(card.cardEffectHub);
    }

    public void ActivateMechanism(Card card)
    {
        mechanismsActivated.Add(card.cardEffectHub);
        activeEffects.Add(card.cardEffectHub);
    }

}
