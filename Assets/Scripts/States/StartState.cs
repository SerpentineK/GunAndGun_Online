using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartState : MonoBehaviour, IState
{
    [SerializeField] private GameManager GM;
    [SerializeField] private StartSceneManager SSM;

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

    private SelectionPattern selectionPattern;

    public void DetermineSelectionPattern()
    {
        if (SSM.cardSetNumber == GameManager.CARD_SETS.SINGLE)
        {
            if (SSM.selectionTurn == GameManager.SELECTION_TURN.FIRST)
            {
                selectionPattern = SelectionPattern.SINGLE_FIRST;
            }
            else if (SSM.selectionTurn == GameManager.SELECTION_TURN.SECOND)
            {
                selectionPattern = SelectionPattern.SINGLE_SECOND;
            }
        }
        else if (SSM.cardSetNumber == GameManager.CARD_SETS.DOUBLE)
        {
            selectionPattern = SelectionPattern.DOUBLE;
        }
    }

    public void EnterState()
    {
        // ���[�f�B���O��ʂ���������̂ŁA�����\������t�F�[�Y
        phase = 0;
        DetermineSelectionPattern();
    }
    public void ExitState()
    {

    }

    public void InState()
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
    }


}
