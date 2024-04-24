using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{

    // プレイヤーに帰属するマネージャー
    public FieldManager FM;
    public EffectManager EM;
    public SkillManager SM;

    // StartSceneにて選択されたデータ
    public GunnerData gunnerData;
    public GunsData rightGunsData;
    public GunsData leftGunsData;
    public SkillData skillData;

    // 視点のプレイヤーか否か
    public bool isProtagonist;

    // ターンプレイヤーか否か
    public bool isTurn;

    // ヒットポイント
    public int HP = 30;
    public TMP_Text HP_Counter;

    // カウンター類
    [HideInInspector] public int burnCounters = 0;
    [HideInInspector] public int shockCounters = 0;


    // バフ・デバフ
    // （同名カード使用制限や射撃のダメージを増やしたりする【銃弾】は入れると多すぎるからひとまず除外、【機能】はいったん全部入れる）

    // スタックする、つまり同時に重ね掛けできるバフ・デバフ（【機能】はだいたいこっち）
    [HideInInspector] public int stabilizers = 0; // スタビライザー（自ターン終了時、【銃弾】を捨て札か手札から1枚まで装填）
    [HideInInspector] public int snipers = 0; // スナイプ（そのターン中相手にHITを与えるたび1ハンデス）
    [HideInInspector] public int spiralShooters = 0; // スパイラルシューター（自分の射撃によって与えるHITを1増やす）
    [HideInInspector] public int autoCannons = 0; // オートカノン（自ターン開始時相手に5HIT、デッキに1枚しかないが原理的にはスタック可能）
    [HideInInspector] public int healingTubes = 0; // ヒーリングチューブ（自ターン終了時回復1）
    [HideInInspector] public int reloadSupporters = 0; // リロードサポーター（自ターン終了時デッキの上から1枚まで装填）
    [HideInInspector] public int psNegatives = 0; // PS/Negative（相手が銃士効果以外で回復した際回復量と同じ枚数相手の山札を捨て札へ、重ね掛け可能と思われる）
    [HideInInspector] public int boostSets = 0; // ブーストセット（自ターン終了時、デッキの上から3枚までそれぞれ表か裏向きで装填）
    [HideInInspector] public int luckySets = 0; // ラッキーセット（自ターン終了時、デッキの上から1枚まで表か裏向きで装填）
    [HideInInspector] public int maintenanceCores = 0; // メンテナンスコア（自ターン開始時、このカードを捨てて5回復）
    [HideInInspector] public int endorthermicStructures = 0; // 吸熱機構（相手が1HIT以上与えられたとき1回復）
    [HideInInspector] public int dreadAmps = 0; // ドレッドアンプ（自ターン終了時、任意の数のプレイヤーを選んで両デッキの上から1枚をVOLTへ送る）
    [HideInInspector] public int highMetronomes = 0; // ハイメトロノーム（自ターン開始時、全プレイヤーに2ハンデスと自分が選んだデッキから2ドロー）
    [HideInInspector] public int redLightSystems = 0; // 紅燐光器（ボルテージの枚数が相手より多いなら、自分の射撃によって与えるHITを1増やす）
    [HideInInspector] public int chainReloaders = 0; // チェインリローダー（自ターン終了時、どちらかのデッキの上から2枚を公開し、1枚を裏向きで装填、1枚を手札に加える）
    [HideInInspector] public int spinShields = 0; // スピンシールド（相手の射撃によって与えるHITを1減らす）
    [HideInInspector] public int accumulationDevices = 0; // 蓄電機構（自ターン終了時、デッキの上から1枚まで装填）
    [HideInInspector] public int reactorSystems = 0; // 反応機構（相手が銃士効果以外で山札からドローした際相手に2HIT）
    [HideInInspector] public int highGravitySpheres = 0; // 多重力光球（相手の射撃によって3HIT以上与えられるとき、代わりに【機能】として発動済みのこのカードをデッキの下へ置いてよい）
    [HideInInspector] public int overPressuredSpheres = 0; // 過負荷光球（自ターン開始時、相手のデッキの上から1枚捨て札に置く）

    // スタックしないバフ・デバフ
    [HideInInspector] public bool burst = false; // 【技能】のバースト効果成立条件達成
    [HideInInspector] public bool unconsicious = false; // 昏倒弾（カードの効果による装填を禁止）
    [HideInInspector] public bool staggered = false; // ダイダラの機銃効果およびオーバークロック（前者は片方、後者は両方の機銃の射撃禁止）
    [HideInInspector] public bool echoing = false; // エコーバレット（【技能】の発動禁止）
    [HideInInspector] public bool detonated = false; // カシャの機銃効果（【技能】の発動禁止）
    [HideInInspector] public bool frozen = false; // フリーズバレット（カードの効果以外での装填禁止）
    [HideInInspector] public bool poisoned = false; // ポイズンバレット（特定のデッキからのドロー禁止）
    [HideInInspector] public bool intercept = false; // インターセプト（射撃ダメージ無効化）
    [HideInInspector] public bool smoke = false; // スモーク（全ダメージ無効化）
    [HideInInspector] public bool overheat = false; // オーバーヒート（カードの効果によるドローの禁止）
    [HideInInspector] public bool barrage = false; // バラージ（相手の【対応】以外のカード効果を無効化）
    [HideInInspector] public bool limited_Action = false; // 【リミット：行動】
    [HideInInspector] public bool limited_Bullet = false; // 【リミット：銃弾】
    [HideInInspector] public bool disturbedByNoise = false; // ノイズジェネレータ（ターンに一枚までに【行動】を制限）
    [HideInInspector] public bool blitz = false; // ブリッツ（ターン終了時、ターンを繰り返す）
    [HideInInspector] public bool paralyzed = false; // パラライズバレット（【行動】禁止）
    [HideInInspector] public bool minorError = false; // エラーパッチ（カードの効果による装填が行われた際に発生する射撃禁止、装填前の状態）
    [HideInInspector] public bool fatalError = false; // エラーパッチ（カードの効果による装填が行われた際に発生する射撃禁止、装填後の状態）
    [HideInInspector] public bool silenced = false; // 凶弾・静（【行動】禁止）
    [HideInInspector] public bool released = false; // 凶強・解（【リミット】解除）
    [HideInInspector] public bool bashed = false; // 叩きつけ（片方の機銃の射撃禁止）
    [HideInInspector] public bool immortal = false; // 不撓不屈（効果発動中敗北しない）
    [HideInInspector] public bool curtainCall = false; // カーテンコール（効果発動中敗北しない）
    [HideInInspector] public bool modePhalanx = false; // モード・ファランクス（自ターン終了時相手に5HIT、その後自分のどちらかのデッキの上から5枚を捨て札へ） 
    [HideInInspector] public bool jamming = false; // ジャミング（対応された相手の射撃または技能の「HITを与える以外の効果」をメインフェイズ終了時まで無効にする）
    [HideInInspector] public bool recharging = false; // リチャージャー（相手ターン終了時、手札が5枚になるようにカードを引く）


    // 手札枚数
    public int handNum;

    // このプレイヤーの銃士、機銃、技能オブジェクト
    public Gunner gunner;
    public Gun rightGun;
    public Gun leftGun;
    public Skill skill;

    public void InputPlayerData() 
    {
        gunner.data = gunnerData;
        gunner.InputGunnerData();
        rightGun.data = rightGunsData;
        rightGun.InputGunData();
        leftGun.data = leftGunsData;
        leftGun.InputGunData();
        skill.data = skillData;
        skill.InputSkillData();
        HP = 30;
        HP_Counter.SetText(string.Format("{0:00}", HP));
        handNum = gunner.hand;
    }
 
    
    public void DrawCardsAsRule()
    {
        int currentHandNum = FM.hand.cardCount;
        int numToDraw = handNum - currentHandNum;
        if (numToDraw > 0)
        {
            FM.DrawFromDeck(numToDraw,FM.leftDeck);
        }
    }

    public void PlayCardFromHand(Card card)
    {
        if (card.currentField != FM.hand)
        {
            return;
        }
        Card.CardStatus status = card.ExamineBeforePlay();
        if (status == Card.CardStatus.PLAYABLE)
        {
            if (card.cardType == CardData.CardType.Action)
            {
                EM.UseAction(card);
                FM.TransferCard(card.currentField, FM.discard, card);
            }
            else if (card.cardType == CardData.CardType.Reaction)
            {
                EM.SetReaction(card);
                FM.TransferCard(card.currentField, FM.set, card);
            }
            else if (card.cardType == CardData.CardType.Mechanism)
            {
                EM.ActivateMechanism(card);
                FM.TransferCard(card.currentField, FM.mechanism, card);
            }
        }
    }
}
