using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// ローカルにのみ存在する、各プレイヤーの情報表示用クラス。
// インスタンスは環境につき2つ、つまり自機と敵機に1つずつ。
// このクラスはゲームの挙動に影響を与えることはなく、ただ指令を受けて表示を変えるだけ。
public class PlayerDisplay : MonoBehaviour
{
    // プレイヤー名
    public string nickName = "";

    // このプレイヤーの銃士、機銃、技能オブジェクト
    public Gunner gunner;
    public Gun rightGun;
    public Gun leftGun;
    public Skill skill;

    // ターンプレイヤーか否か
    public bool isTurn;

    // プレイヤーの各種表示
    public TMP_Text nicknameDisplay;
    public TMP_Text HP_Display;
}
