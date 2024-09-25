using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MenuStateManager;

public class MenuStateController : MonoBehaviour
{
    [Header("States")]
    public InitialMenuState initialS_class;
    public BattleStandbyState battleStandbyS_class;
    public BossStandbyState bossStandbyS_class;

    [HideInInspector] public IState initialS, battleStandbyS, bossStandbyS;

    private IState nowState;


    void Start()
    {
        SetStates();
        SwitchStates(initialS);
    }

    void SetStates()
    {
        ToggleGameObject(initialS_class.gameObject, false);
        initialS = initialS_class;
        ToggleGameObject(battleStandbyS_class.gameObject, false);
        battleStandbyS = battleStandbyS_class;
        ToggleGameObject(bossStandbyS_class.gameObject, false);
        bossStandbyS = bossStandbyS_class;
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
