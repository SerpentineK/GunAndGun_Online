using System.Collections;
using System.Collections.Generic;
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

    private int phase;

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
        INITIAL,
        WAITING,
        GUNNER_SELECTION,
        GUN01_SELECTION,
        GUN02_SELECTION,
        SKILL_SELECTION
    }

    private SelectionPattern selectionPattern;

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

    private SUB_STATE[] myPattern;

    IEnumerator<SUB_STATE> enumerator;

    public void DeterminePattern()
    {
        switch (SSM.cardSetNumber)
        {
            case GameManager.CARD_SETS.SINGLE:
                switch (SSM.selectionTurn)
                {
                    case GameManager.SELECTION_TURN.FIRST:
                        myPattern = pattern_Single01;
                        break;
                    case GameManager.SELECTION_TURN.SECOND:
                        myPattern = pattern_Single02;
                        break;
                }
                break;
            case GameManager.CARD_SETS.DOUBLE:
                myPattern = pattern_Double;
                break;
        }
    }

    public void EnterState()
    {
        // ローディング画面をいずれ作るので、それを表示するフェーズ
        phase = 0;
        DeterminePattern();
        enumerator = myPattern.GetEnumerator() as IEnumerator<SUB_STATE>;
    }
    public void ExitState()
    {

    }

    public void InState()
    {
        if (enumerator.Current == SUB_STATE.INITIAL)
        {
            SSM.InitializeViews();
            enumerator.MoveNext();
            SSM.ChangeToView(enumerator.Current);
        }
        else if (enumerator.Current == SUB_STATE.GUNNER_SELECTION) 
        {
            if (SSM.GunnerToken) 
            {
                enumerator.MoveNext();
                SSM.ChangeToView(enumerator.Current);
            }
        }
        else if (enumerator.Current == SUB_STATE.GUN01_SELECTION)
        {
            if (SSM.FirstGunToken)
            {
                enumerator.MoveNext();
                SSM.ChangeToView(enumerator.Current);
            }
        }
        else if (enumerator.Current == SUB_STATE.GUN02_SELECTION)
        {
            if (SSM.SecondGunToken)
            {
                enumerator.MoveNext();
                SSM.ChangeToView(enumerator.Current);
            }
        }
        else if (enumerator.Current == SUB_STATE.WAITING) 
        {
            if (SSM.mySelection.IsSelectionTurn)
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
