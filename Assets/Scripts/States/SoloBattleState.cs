using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoloBattleState : MonoBehaviour
{
    public SoloStateManager SSM;

    public enum SUB_STATE
    {
        INITIAL_WAIT,
        START_BATTLE,
        START_TURN,
        IDLE,
        USE_DECKCARD,
        USE_SKILL,
        RELOAD_GUN,
        FIRE_GUN,
        END_TURN,
        BOSS_TURN,
        END_BATTLE
    }

    private SUB_STATE subState;
    private int phase;

    public void EnterState()
    {
        subState = SUB_STATE.INITIAL_WAIT;
        phase = 0;
    }
    public void ExitState()
    {

    }

    public void InState()
    {
        if (subState == SUB_STATE.INITIAL_WAIT)
        {
            InInitialWait();
        }
        else if (subState == SUB_STATE.START_BATTLE)
        {
            InStartBattle();
        }
    }

    private void InInitialWait()
    {
        if (phase == 0)
        {
            
            phase++;
        }
        else if (phase == 1)
        {
            subState = SUB_STATE.START_BATTLE;
            phase = 0;
        }
    }

    private void InStartBattle()
    {
        if (phase == 0)
        {
            
            phase++;
        }
    }

    private void InIdle()
    {

    }

    private void InUseDeckCard()
    {

    }
}
