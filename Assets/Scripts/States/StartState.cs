using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartState : MonoBehaviour, IState
{
    [SerializeField] private GameManager GM;
    [SerializeField] private StartStateManager SSM;

    // �Q�[�����[�h(1�Z�b�g�A2�Z�b�g�A�A�����~�e�b�h1�Z�b�g�A�A�����~�e�b�h2�Z�b�g)��
    // �J�[�h�v�[��(2�u���b�N4���)�̌���͑ΐ핔�������Ă�i�K�ł�肽���B
    // �i�ǂ�����đΐ�n�܂��Ă��猈�߂�΂������킩��Ȃ��̂Łj

    // ����StartState�ɂ����čs�������̂́A
    // 1: (1�Z�b�g�̏ꍇ)�R�C���g�X
    // 2: �e�m�̌���
    // 3: �@�e�̌���
    // 4: �Z�\�̌���
    // 5: �퓬��ʂւ̈ڍs

    private int phase;

    // �J�[�h�Z�b�g�̐��ɂ���ďe�m�E�@�e�̑I�������قȂ邽�߁A
    // �ȉ��̕ϐ��ɂ���ăJ�[�h�Z�b�g�̐��Ɛ����𔻒肷��B
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
        // ���[�f�B���O��ʂ���������̂ŁA�����\������t�F�[�Y
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
