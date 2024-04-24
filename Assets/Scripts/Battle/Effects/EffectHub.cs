using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// デッキカード効果や銃士効果など、「一纏めになった効果」のクラス。
// EffectではなくContinuousEffectを継承しているのは、HubをContinuousEffectとして扱いたい場合が存在するため。
// (銃士効果は継続効果なのでContinuousとしてactiveEffectsに登録しておきたいが、内容が複雑で処理が面倒...など)
// 【行動】カード使用時を例にとると、EffectHubの効果解決方法は

// 1: PlayerManagerあたりがカードに紐づいたEffectHub(【行動】なのでcueはImmediate)をactiveEffectsに登録する
// 2: 即座にEffectHubを登録した何らかのManagerがEffectManagerにImmediateのEventCueを送る(EffectManager.csのRecieveCue()関数)
// 3: EffectManagerがactiveEffects内を検索し、cueがImmediateのものを取ってくる
// 4: EffectManagerがEffectHub内の処理を実行する
// 5: EffectManagerが処理の終了したEffectHubをactiveEffectsから削除する

// となる。
// 【対応】なら1がreactionsSetへの登録、2が相手の射撃時/技能使用時「UponOpponentGunFire」のEventCue送信に変化する。
// 【機能】ならcueが個別に設定してあるため発動時点でHubをContinuousEffectとして扱い、activeEffectsに登録する。
// 【銃弾】なら装填時にspecialBulletsLoadedに登録し、Cueがきたら（だいたい「このカードが装填されている機銃の射撃」がトリガー）発動。
[Serializable]
[CreateAssetMenu(fileName = "EffectHub", menuName = "CreateEffectHub")]
public class EffectHub : ContinuousEffect
{
    [HideInInspector]
    // Hubが結び付いているカード本体
    public Card thisCard;

    [HideInInspector]
    // 同じく機銃本体
    public Gun thisGun;

    [HideInInspector]
    // 銃士本体
    public Gunner thisGunner;

    [HideInInspector]
    // 技能本体
    public Skill thisSkill;

    // 格納しているEffectのリスト
    public List<Effect> effects;

    // カードを使わずとも自動的に発動する効果のHub
    // (《オーバーヒート》の第一ターン使用不可など)
    public ContinuousEffect automaticEffect;

    // 上の自動効果を解除する効果
    public EndContinuous automaticEndEffect;

    // Hubが紐づいているカード/銃士/機銃/技能
    public ScriptableObject attachedData;

    // InstantEffectではないため直接発動はさせないが、関わりのある効果
    public List<Effect> ExtraEffects;

    // カード使用に1ターン1回の制限がかかっているか否かのbool値
    public bool usageIsOncePerTurn = false;

    // 効果発動に1ターン1回の制限がかかっているか否かのbool値
    // 上がfalseでこれがtrueの場合、カード使用はできるが効果は発動しない(《ポイズンバレット》など)
    public bool activationIsOncePerTurn = false;

    // ヒカギリの【リミット】用のbool値
    public bool isLimitedByHikagiri = false;

    // 手札誘発するか否かのbool値(するのは事実上チューニング・ボムだけ)
    public bool effectTriggersFromHand = false;

    // オーバークロックカードか否か
    public bool isOverclock = false;

    // 同一カード内で「対象を選び、対象についてのみ効果発動」というテキストがある場合、
    // 今回の実装では対象選択と効果発動は別々のEffectなのでEffectより上位のオブジェクトに
    // 受け渡し用のリストを用意する必要がある。

    // プレイヤー編
    [HideInInspector]
    public Player[] playerResults;

    // 子Effectが処理時に発動するか(Effectで発動の有無を判定し、その値をここに返し、その後次のEffectがこの値を参照する)
    [HideInInspector]
    public bool childrenOperateOnActivation = true;

    // テキスト書き換えを行うか(Effectでテキスト書き換えの有無を判定し、その値をここに返し、その後次のEffectがこの値を参照する)
    [HideInInspector]
    public bool childrenOperateAsAltEffect;

    // Effect編
    [HideInInspector]
    public Effect[] effectResults;


    // カード編
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

    // 機銃編
    [HideInInspector]
    public Gun[] gunResults01;
    [HideInInspector]
    public Gun[] gunResults02;

    // デッキ編
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

    // 数値編
    [HideInInspector]
    public NumeralValue intResult01;
    [HideInInspector]
    public NumeralValue intResult02;

    // projectile編
    [HideInInspector]
    public FiredProjectile projectileResult;

    // EffectHub編
    [HideInInspector]
    public EffectHub hubResult01;
    [HideInInspector]
    public EffectHub hubResult02;

    // Data編
    [HideInInspector]
    public ScriptableObject dataResult01;
    [HideInInspector]
    public ScriptableObject dataResult02;

    //Skill編
    [HideInInspector]
    public Skill skillResult;

    // カード種類編
    [HideInInspector]
    public CardData.CardType cardTypeResult = CardData.CardType.Other;

    // (開発用)記入済みか否か
    public bool finishedInput;
}
