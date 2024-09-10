using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    
    [Header("States")]
    public StartState startS_class;
    public BattleState playS_class;

    [HideInInspector] public IState startS, playS;

    private IState nowState;


    void Start()
    {
        SetStates();
        SwitchStates(playS);
    }

    void SetStates()
    {
        startS = startS_class;
        playS = playS_class;
    }

    void Update()
    {
        nowState?.InState();
    }

    public void SwitchStates(IState nextState)
    {
        nowState?.ExitState();
        nextState?.EnterState();
        nowState = nextState;
    }
} 


