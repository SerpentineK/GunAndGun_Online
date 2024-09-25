using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        INITIAL,            // �ŏ��A�\�����g�݂�����O�̑ҋ@����
        WAITING,            // ����̑I��ҋ@
        GUNNER_SELECTION,   // �e�m�I��
        GUN01_SELECTION,    // �E��@�e�I��
        GUN02_SELECTION,    // ����@�e�I��
        SKILL_SELECTION     // �Z�\�I��
    }

    private SelectionPattern selectionPattern;

    // SINGLE���[�h���̃p�^�[��
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

    // SINGLE���[�h���̃p�^�[��
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

    // DOUBLE���[�h�̃p�^�[��
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

    // IEnumerator��MoveNext()��Current�Ŋe�p�^�[���̐i�s���Ǘ�����B
    private IEnumerator<SUB_STATE> enumerator = null;

    // Online���ۂ��𔻕ʂ��AOnline�łȂ����InState�̏����������������B
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
        // ���[�f�B���O��ʂ���������̂ŁA�����\������t�F�[�Y
        DeterminePattern();
        enumerator = myPattern.GetEnumerator();
    }
    public void ExitState()
    {

    }

    public void InState()
    {
        // Online���ۂ��𔻕ʂ��AOnline�łȂ����InState�̏����������������B
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
