using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartState : MonoBehaviour, IState
{
    [SerializeField] private GameManager GM;
    [SerializeField] private StartStateManager SSM;

    // ゲームモード(1セット、2セット、アンリミテッド1セット、アンリミテッド2セット)と
    // カードプール(2ブロック4種類)の決定は対戦部屋を建てる段階でやりたい。
    // （どうやって対戦始まってから決めればいいかわからないので）

    // このStartStateにおいて行いたいのは、
    // 1: (1セットの場合)コイントス
    // 2: 銃士の決定
    // 3: 機銃の決定
    // 4: 技能の決定
    // 5: 戦闘画面への移行


    // カードセットの数によって銃士・機銃の選択順が異なるため、
    // 以下の変数によってカードセットの数と先手後手を判定する。
    public enum SelectionPattern
    {
        SINGLE_FIRST,
        SINGLE_SECOND,
        DOUBLE
    }

    public enum SUB_STATE
    {
        INITIAL,            // 最初、表示が組みあがる前の待機時間
        WAITING,            // 相手の選択待機
        GUNNER_SELECTION,   // 銃士選択
        GUN01_SELECTION,    // 右手機銃選択
        GUN02_SELECTION,    // 左手機銃選択
        SKILL_SELECTION     // 技能選択
    }

    private SelectionPattern selectionPattern;

    // SINGLEモード先手のパターン
    private readonly SUB_STATE[] pattern_Single01 = 
    {
        SUB_STATE.INITIAL,
        SUB_STATE.GUNNER_SELECTION,
        SUB_STATE.WAITING,
        SUB_STATE.GUN01_SELECTION,
        SUB_STATE.WAITING,
        SUB_STATE.WAITING,
        SUB_STATE.GUN02_SELECTION,
        SUB_STATE.SKILL_SELECTION
    };

    // SINGLEモード後手のパターン
    private readonly SUB_STATE[] pattern_Single02 =
    {
        SUB_STATE.INITIAL,
        SUB_STATE.WAITING,
        SUB_STATE.GUNNER_SELECTION,
        SUB_STATE.WAITING,
        SUB_STATE.GUN01_SELECTION,
        SUB_STATE.GUN02_SELECTION,
        SUB_STATE.WAITING,
        SUB_STATE.SKILL_SELECTION
    };

    // DOUBLEモードのパターン
    private readonly SUB_STATE[] pattern_Double =
    {
        SUB_STATE.INITIAL,
        SUB_STATE.GUNNER_SELECTION,
        SUB_STATE.WAITING,
        SUB_STATE.GUN01_SELECTION,
        SUB_STATE.WAITING,
        SUB_STATE.GUN02_SELECTION,
        SUB_STATE.WAITING,
        SUB_STATE.SKILL_SELECTION
    };

    private List<SUB_STATE> myPattern;

    // IEnumeratorのMoveNext()とCurrentで各パターンの進行を管理する。
    private IEnumerator<SUB_STATE> enumerator = null;

    // Onlineか否かを判別し、OnlineでなければInStateの処理を回避し続ける。
    public static bool isOnline = false;

    public void DeterminePattern()
    {
        switch (SSM.cardSetNumber)
        {
            case GameManager.CARD_SETS.SINGLE:
                switch (SSM.selectionTurn)
                {
                    case GameManager.SELECTION_TURN.FIRST:
                        myPattern = pattern_Single01.ToList();
                        break;
                    case GameManager.SELECTION_TURN.SECOND:
                        myPattern = pattern_Single02.ToList();
                        break;
                }
                break;
            case GameManager.CARD_SETS.DOUBLE:
                myPattern = pattern_Double.ToList();
                break;
        }
    }

    public void EnterState()
    {
        // ローディング画面をいずれ作るので、それを表示するフェーズ
        DeterminePattern();
        enumerator = myPattern.GetEnumerator();
    }
    public void ExitState()
    {

    }

    public void InState()
    {
        // Onlineか否かを判別し、OnlineでなければInStateの処理を回避し続ける。
        if (!isOnline)
        {
            return;
        }

        if (enumerator?.Current == SUB_STATE.INITIAL)
        {
            SSM.InitializeViews();
            enumerator.MoveNext();
            SSM.ChangeToView(enumerator.Current);
        }
        else if (enumerator?.Current == SUB_STATE.GUNNER_SELECTION) 
        {
            if (SSM.mySelection.isFinished) 
            {
                enumerator.MoveNext();
                SSM.ChangeToView(enumerator.Current);
            }
        }
        else if (enumerator?.Current == SUB_STATE.GUN01_SELECTION)
        {
            if (SSM.mySelection.isFinished)
            {
                enumerator.MoveNext();
                SSM.ChangeToView(enumerator.Current);
            }
        }
        else if (enumerator?.Current == SUB_STATE.GUN02_SELECTION)
        {
            if (SSM.mySelection.isFinished)
            {
                enumerator.MoveNext();
                SSM.ChangeToView(enumerator.Current);
            }
        }
        else if (enumerator?.Current == SUB_STATE.WAITING) 
        {
            if (SSM.theirSelection.isFinished)
            {
                enumerator.MoveNext();
                SSM.ChangeToView(enumerator.Current);
            }
        }
    }

    /* public void InState()
    {
        if (phase == 0) 
        {
            SSM.InitializeViews();
            phase++; 
            SSM.ChangeToView(phase);
        }
        else if (phase == 1)
        {
            if (SSM.GunnerToken)
            {
                phase++;
                SSM.ChangeToView(phase);
            }
        }
        else if (phase == 2)
        {
            if (SSM.FirstGunToken) 
            {
                phase++;
                SSM.ChangeToView(phase);
            }
        }
        else if(phase == 3)
        {
            if (SSM.SecondGunToken)
            {
                phase++;
                SSM.ChangeToView(phase);
            }
        }
        else if (phase == 4)
        {
            if (SSM.SkillToken)
            {
                
            }
        }
    } */


}
