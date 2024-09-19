using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class GameManager : NetworkBehaviour
{
    [SerializeField] private GunnerDatabase gunnerDataBase;
    [SerializeField] private GunsDatabase gunsDataBase;

    public enum GAME_MODE
    {
        NORMAL,
        UNLIMITED
    }

    public enum CARD_SETS
    {
        SINGLE,
        DOUBLE
    }

    public enum CARD_POOL
    {
        GunAndGun,
        OverHeat,
        WShout,
        UltraBommy
    }

    public enum CARD_BLOCK 
    {
        BLOCK_01,
        BLOCK_02,
        CUSTOM
    }

    public enum SELECTION_TURN 
    {
        FIRST,
        SECOND,
        SIMULTANEOUS
    }

    public static GAME_MODE gameMode = GAME_MODE.NORMAL;
    public static CARD_SETS cardSets = CARD_SETS.SINGLE;
    public static List<CARD_POOL> cardPools;
    public static SELECTION_TURN selectionTurn;
}
