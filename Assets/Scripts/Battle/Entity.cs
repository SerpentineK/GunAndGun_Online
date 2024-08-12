using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Fusion;

// ゲーム内においてカードを使いゲームをプレイする主体として扱える存在すべてを内包するクラス。
// ボスとプレイヤーを同じ枠組みで扱いたいという気持ちから生まれた。
public class Entity : NetworkBehaviour
{
    // ヒットポイント
    public int HP = 30;
    public TMP_Text HP_Counter;

    // カウンター類
    [HideInInspector] public int burnCounters = 0;
    [HideInInspector] public int shockCounters = 0;

    // 手札枚数
    public int handNum;

    // バフ・デバフ
    // （同名カード使用制限や射撃のダメージを増やしたりする【銃弾】は入れると多すぎるからひとまず除外、【機能】はいったん全部入れる）
    public enum STATUS_ANOMALY
    {
        None = 0,

        // スタックする、つまり同時に重ね掛けできるバフ・デバフ（【機能】はだいたいこっち）
        Stabilizer, // スタビライザー（自ターン終了時、【銃弾】を捨て札か手札から1枚まで装填）
        Sniper, // スナイプ（そのターン中相手にHITを与えるたび1ハンデス）
        SpiralShooter, // スパイラルシューター（自分の射撃によって与えるHITを1増やす）
        AutoCannon, // オートカノン（自ターン開始時相手に5HIT、デッキに1枚しかないが原理的にはスタック可能）
        HealingTube, // ヒーリングチューブ（自ターン終了時回復1）
        ReloadSupporter, // リロードサポーター（自ターン終了時デッキの上から1枚まで装填）
        PSNegative, // PS/Negative（相手が銃士効果以外で回復した際回復量と同じ枚数相手の山札を捨て札へ、重ね掛け可能と思われる）
        BoostSet, // ブーストセット（自ターン終了時、デッキの上から3枚までそれぞれ表か裏向きで装填）
        LuckySet, // ラッキーセット（自ターン終了時、デッキの上から1枚まで表か裏向きで装填）
        MaintainanceCore, // メンテナンスコア（自ターン開始時、このカードを捨てて5回復）
        EndorthermicStructure, // 吸熱機構（相手が1HIT以上与えられたとき1回復）
        DreadAmp, // ドレッドアンプ（自ターン終了時、任意の数のプレイヤーを選んで両デッキの上から1枚をVOLTへ送る）
        HighMetronome, // ハイメトロノーム（自ターン開始時、全プレイヤーに2ハンデスと自分が選んだデッキから2ドロー）
        RedlightSystem, // 紅燐光器（ボルテージの枚数が相手より多いなら、自分の射撃によって与えるHITを1増やす）
        ChainReloader, // チェインリローダー（自ターン終了時、どちらかのデッキの上から2枚を公開し、1枚を裏向きで装填、1枚を手札に加える）
        SpinShield, // スピンシールド（相手の射撃によって与えるHITを1減らす）
        AccumulationDevice, // 蓄電機構（自ターン終了時、デッキの上から1枚まで装填）
        ReactorSystem, // 反応機構（相手が銃士効果以外で山札からドローした際相手に2HIT）
        HighGravitySphere, // 多重力光球（相手の射撃によって3HIT以上与えられるとき、代わりに【機能】として発動済みのこのカードをデッキの下へ置いてよい）
        OverpressuredSphere, // 過負荷光球（自ターン開始時、相手のデッキの上から1枚捨て札に置く）

        // スタックしないバフ・デバフ
        Burst, // 【技能】のバースト効果成立条件達成
        Unconscious, // 昏倒弾（カードの効果による装填を禁止）
        Staggered, // ダイダラの機銃効果およびオーバークロック（前者は片方、後者は両方の機銃の射撃禁止）
        Echoing, // エコーバレット（【技能】の発動禁止）
        Detonated, // カシャの機銃効果（【技能】の発動禁止）
        Frozen, // フリーズバレット（カードの効果以外での装填禁止）
        Poisoned, // ポイズンバレット（特定のデッキからのドロー禁止）
        Intercept, // インターセプト（射撃ダメージ無効化）
        Smoke, // スモーク（全ダメージ無効化）
        Overheat, // オーバーヒート（カードの効果によるドローの禁止）
        Barrage, // バラージ（相手の【対応】以外のカード効果を無効化）
        Limited_Action, // 【リミット：行動】
        Limited_Bullet, // 【リミット：銃弾】
        DisturbedByNoise, // ノイズジェネレータ（ターンに一枚までに【行動】を制限）
        Blitz, // ブリッツ（ターン終了時、ターンを繰り返す）
        Paralyzed, // パラライズバレット（【行動】禁止）
        MinorError, // エラーパッチ（カードの効果による装填が行われた際に発生する射撃禁止、装填前の状態）
        FatalError, // エラーパッチ（カードの効果による装填が行われた際に発生する射撃禁止、装填後の状態）
        Silenced, // 凶弾・静（【行動】禁止）
        Released, // 凶強・解（【リミット】解除）
        Bashed, // 叩きつけ（片方の機銃の射撃禁止）
        Immortal, // 不撓不屈（効果発動中敗北しない）
        CurtainCall, // カーテンコール（効果発動中敗北しない）
        ModePhalanx, // モード・ファランクス（自ターン終了時相手に5HIT、その後自分のどちらかのデッキの上から5枚を捨て札へ） 
        Jamming, // ジャミング（対応された相手の射撃または技能の「HITを与える以外の効果」をメインフェイズ終了時まで無効にする）
        Recharging // リチャージャー（相手ターン終了時、手札が5枚になるようにカードを引く）
    }

    public List<STATUS_ANOMALY> statusList = new();
    
}
