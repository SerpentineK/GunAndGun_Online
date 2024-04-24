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


    // 効果内容のstring
    [TextArea(3, 10)]
    public string effectContentText;

    /// <summary>
    /// 効果発動のトリガーとなる現象enum
    /// 
    /// 基本的に見た通りの意味。
    /// PlayerCommandは「プレイヤーが望むなら自ターン中いつでも発動できる効果（コストを支払える時の【行動】やヒバナの銃士効果など）」を指す。
    /// 
    /// 《バレットキャッチ》、《キミのためなら死ねる》などの継続効果については（前者はUponOpponentGunFire、後者はTakenDamageという具合に）個別にトリガーを設定し、
    /// Constantは常時プレイヤーステータスに影響し続ける効果（《ダブルバレット》や《パラライズバレット》、マカの銃士効果など）に限定したい。
    /// 
    /// なお、射撃のHITへの修正は「すでに発動している永続効果（バレットキャッチなど）＞特殊銃弾＞対応カード」という順番で発生するらしい。
    /// (出典：https://w.atwiki.jp/ganandganinformal/pages/23.html)
    /// 同じEventCueを有する効果の中でも処理順が存在することに注意。
    /// </summary>
    public enum EventCue
    {
        // 常にステータスに影響を与える効果
        Constant,

        // ターンプレイヤーが任意で発動できる効果
        PlayerCommand,

        // カード使用時効果
        // (特段効果発動についてタイミングの指定がないカードのEffectHubや、EffectHubに内蔵されるEffectはすべてこれ)
        // 対応についても(UponPlayerGunFireではなく)これ。
        Immediate,

        // 前の効果に引き続き即座に発動する効果
        Successive,

        // 「装填時」効果
        PlayerReload,
        OpponentReload,

        // 「射撃時」効果
        UponPlayerGunFire,
        UponOpponentGunFire,

        // 「射撃した場合」効果
        AfterPlayerGunFire,
        AfterOpponentGunFire,

        // 「射撃でHITを与えた場合」効果
        GivenDamageByThisGunsFire,

        // 【技能】使用時効果
        // （【対応】と【技能】を想定）
        PlayerSkillUsage,
        OpponentSkillUsage,

        // 「次のターン」効果
        // （「自分のターンをもう一度行う」という効果のカードが存在するため、次のターンが相手のターンであるとは限らない）
        NextTurnStart,
        NextTurnEnd,

        // 「自他のターン開始時/終了時」効果
        PlayerTurnStart,
        OpponentTurnStart,
        PlayerTurnEnd,
        OpponentTurnEnd,

        // 「カードを引いたとき」効果
        PlayerDraw,
        OpponentDraw,

        // 「HITを与えた/与えられた場合」効果
        GivenDamage,
        TakenDamage,

        // ゲーム開始時効果
        StartedGame,

        // 永続効果のendCue用
        Never,

        // このターンの終了時
        ThisTurnEnd,

        // このカードを装填した機銃の射撃時
        ThisCardsLoadedGunFire,

        // この機銃の射撃時
        ThisGunsFire,

        // 現在進行中の処理が全て完了し、ニュートラルな状態に戻ったとき(特定の機銃射撃時のみ対応無効化、などのEnd)
        EndOfCurrentSequence,

        // 相手が回復した時
        OpponentHeal,

        // 相手の機銃が裏向きになった時
        OpponentOverclock,

        // 次のプレイヤーターン終了時
        NextPlayerTurnEnd,

        // 相手が「ダメージを与えられたとき」
        OpponentTakenDamage,

        // このカードが捨て札に置かれたとき
        ThisCardIsDiscarded,

        // 次の相手のターン終了時
        NextOpponentTurnEnd,

        // このカードがボルテージに置かれたとき
        ThisCardIsStoredInVoltage,

        // このターンのメインフェイズ終了時
        ThisTurnsMainPhaseEnd,

        // プレイヤー（自分）の回復
        PlayerHeal,

        // 相手がカードの効果で装填を行ったとき
        OpponentReloadByEffect,

        // 相手が銃士効果以外で回復を行ったとき
        OpponentHealByEffect,

        // 相手が銃士効果以外でカードを引いたとき
        OpponentDrawByEffect,
    }

    // 効果対象の選択肢enum
    public enum EffectTarget
    {
        none,

        // このカード
        ThisCard,

        // このカードの紐づいている機銃
        ThisCardsGun,

        // HP
        PlayerHP,
        OpponentHP,

        // 手札
        PlayerHand,
        OpponentHand,
        
        // 装填されたカード全て
        PlayerBullets,
        OpponentBullets,

        // 山札
        PlayerDecks,
        OpponentDecks,

        // 捨て札
        PlayerDiscard,
        OpponentDiscard,

        // ボルテージ
        PlayerVolt,
        OpponentVolt,

        // 機能
        PlayerMech,
        OpponentMech,

        // 伏せ札(対応)
        PlayerSet,
        OpponentSet,

        // 機銃
        PlayerRightGun,
        PlayerLeftGun,
        OpponentRightGun,
        OpponentLeftGun,

        // 技能
        PlayerSkill,
        OpponentSkill,

        // 銃士の速度
        PlayerGunnerAgility,
        OpponentGunnerAgility,

        // 進行中の処理フェーズそのもの
        CurrentSequence,

        // 進行中の処理フェーズにおいて、処理終了後対象に与えることに現時点でなっているHIT
        // 例えば、この対象を取った《整息》が条件を満たして発動した後にHIT修正が入るかもしれない
        CurrentSequenceHitToPlayer,
        CurrentSequenceHitToOpponent,

        // このカードと同じ名前のカード
        OtherCardsWithSameName,

        // このカードの帰属する機銃の装填
        ThisCardsGunsBullets,

        // このカードの帰属する機銃のデッキ
        ThisCardsGunsDeck,

        // 銃士の手札枚数
        PlayerGunnerHand,
        OpponentGunnerHand,

        // 全てのField
        PlayerAllFields,
        OpponentAllFields,

        // このカードが装填されてる機銃
        ThisCardsLoadedGun,

        // SelectされEffectHubに保管されている値
        SelectedInt,

        // SelectされたGunのDeck
        SelectedGunsDeck,

        // 左右の機銃の装填
        PlayerRightGunsBullets,
        PlayerLeftGunsBullets,
        OpponentRightGunsBullets,
        OpponentLeftGunsBullets,

        // このカードが装填されている機銃の装填
        ThisCardsLoadedMagazine,

        // 現在射撃されている機銃
        CurrentlyFiredGun,

        // 現在実行されている効果
        CurrentlyResolvingEffect,

        // この機銃(およびそのパラメータ)
        ThisGun,
        ThisGunsLoadedBullets,

        // 最終的に与えた/与えられたダメージ
        DamageGiven,
        DamageTaken,

        // SHOCKカウンターの数
        ShockOnOpponent,

        // 相手による回復の量
        HealByOpponent,

        // 以前の効果で選択したカード
        SelectedCardSet01,

        // 各デッキ(両プレイヤーの両方のデッキをそれぞれ指定したい場合に使う)
        PlayerRightDeck,
        PlayerLeftDeck,
        OpponentRightDeck,
        OpponentLeftDeck,

        // このカードが処理前に属していたField
        ThisCardsFormerField,

        // スキルのコスト
        PlayerSkillCost,
        OpponentSkillCost,

        // 選択された機銃の装填
        SelectedGunsBullets,

        // 対応を発動した対象である射撃または技能
        ReactedProjectile,
        ReactedSkill,

        // このカードが属するField
        ThisCardsField
    }

    // カードを検索して結果の一覧を表示するとき、どのような条件で検索するかenum
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

    // 値を引数的な役割として取る効果のとき、Hubのどの値を参照するかのenum
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

    // AltEffectに分岐する条件enum
    public enum AltEffectCondition
    {
        // 分岐なし
        none,
        // デッキ選択時
        DeckIsSelected,
        // 以前の効果で選択されたカードが銃弾である
        CardIsSpecialBullet,
        // 以前の効果で選択されたIntが0である
        IntEqualsZero,
        // BURST効果
        HitpointIs15OrLess,
        // 以前の効果で選択されたカードが行動である
        CardIsAction,
        // 以前の効果でOperateAsAltBoolに返された判定がtrueである
        OperateAsAltBoolIsTrue,
    }

    // 効果の発生元がデッキカードだった場合、その種別を特定するための変数
    // (ダタラの【対応】無効化とか、《バラージ》の【対応】以外無効化とかあるので)
    public CardData.CardType cardType = CardData.CardType.Other;

    // 効果発動時の実際の処理(オーバーライド用)
    public virtual void Resolve() { }

    // 自分のクラス名を返す関数
    public string GetEffectClassName()
    {
        return this.GetType().Name;
    }
}

