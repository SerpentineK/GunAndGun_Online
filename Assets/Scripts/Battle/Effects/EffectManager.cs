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
    /// 現在発動中の効果一覧
    /// </summary>
    // 将来的には、効果処理はこのリストに追加してから行いこのリストからの削除を以て完了するようにしたい
    public List<InstantEffect> ongoingEffects;

    /// <summary>
    /// Activeになっている継続効果の一覧（いまこの瞬間は効果を処理していないものも含む）。
    /// 有効化された【機能】や銃士効果などはここに入る。
    /// </summary>
    public List<ContinuousEffect> activeEffects;

    /// <summary>
    /// 有効化されていない、つまり発動を宣言されたもののまだ実行段階に至っていない継続効果の一覧。
    /// 例えば「ポイズンバレット」のドロー不可効果は相手ターン開始時に有効になるため、「ポイズンバレット」の効果発動段階では有効化されずここに入る。
    /// </summary>
    public List<DormantContinuous> dormantEffects;

    /// <summary>
    /// 現在発動待機中の効果一覧（別カードの発動中に誘発したカードを放り込む用）
    /// </summary>
    public List<Effect> triggeredEffects;

    public List<EffectHub> reactionsSet;
    public List<EffectHub> mechanismsActivated;

    public List<EffectHub> specialBulletsLoadedToRightGun;
    public List<EffectHub> specialBulletsLoadedToLeftGun;

    public EffectHub resolvingHub;
    public Gun firedGun;

    /// <summary>
    /// Effect.ValuesToReferToというenumを用いてEffectHubが持っている情報のうちほしいものを入力すると値が帰ってくる魔法の辞書型リスト。
    /// </summary>
    public Dictionary<Effect.ValuesToReferTo, object> HubDictionary { get; private set; }

    /// <summary>
    /// Effect.EffectTargetというenumを用いて効果を与えたい対象を入力すると対象のインスタンスが帰ってくる奇跡の辞書型リスト。
    /// </summary>
    public Dictionary<Effect.EffectTarget, object> TargetDictionary { get; private set; }


    public void Awake()
    {
        instance = this;
    }

    public void RefreshHubDictionary(EffectHub targetHub) 
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
        };
        HubDictionary = myDict;
        return;
    }

    public void RefreshTargetDictionary()
    {
        List<Field> playerBullets = new()
        {
            myself.FM.leftMagazine,
            myself.FM.rightMagazine
        };
        List<Field> opponentBullets = new()
        {
            opponent.FM.leftMagazine,
            opponent.FM.rightMagazine
        }; 
        List<Field> playerDecks = new()
        {
            myself.FM.leftDeck,
            myself.FM.rightDeck
        };
        List<Field> opponentDecks = new()
        {
            opponent.FM.leftDeck,
            opponent.FM.rightDeck
        };
        List<Field> playerAllFields = new()
        {
            myself.FM.hand,
            myself.FM.rightDeck,
            myself.FM.leftDeck,
            myself.FM.rightMagazine,
            myself.FM.leftMagazine,
            myself.FM.set,
            myself.FM.mechanism,
            myself.FM.discard,
            myself.FM.voltage
        };
        List<Field> opponentAllFields = new()
        {
            opponent.FM.hand,
            opponent.FM.rightDeck,
            opponent.FM.leftDeck,
            opponent.FM.rightMagazine,
            opponent.FM.leftMagazine,
            opponent.FM.set,
            opponent.FM.mechanism,
            opponent.FM.discard,
            opponent.FM.voltage
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
            { Effect.EffectTarget.PlayerSet, myself.FM.set },
            { Effect.EffectTarget.PlayerRightGun, myself.rightGun },
            { Effect.EffectTarget.PlayerLeftGun, myself.leftGun },
            { Effect.EffectTarget.PlayerSkill, myself.skill },
            { Effect.EffectTarget.PlayerGunnerAgility, myself.gunner.agility },
            { Effect.EffectTarget.PlayerGunnerHand, myself.gunner.hand },
            { Effect.EffectTarget.PlayerAllFields, playerAllFields },
            { Effect.EffectTarget.PlayerRightGunsBullets, myself.FM.rightMagazine },
            { Effect.EffectTarget.PlayerLeftGunsBullets, myself.FM.leftMagazine },
            { Effect.EffectTarget.PlayerSkillCost, myself.skill.cost },
            { Effect.EffectTarget.OpponentHP, opponent.HP },
            { Effect.EffectTarget.OpponentHand, opponent.FM.hand },
            { Effect.EffectTarget.OpponentBullets, opponentBullets },
            { Effect.EffectTarget.OpponentDecks, opponentDecks },

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
            RefreshHubDictionary(currentHub);
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
