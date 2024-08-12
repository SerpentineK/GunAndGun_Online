using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class GameManager : NetworkBehaviour
{
    [SerializeField] private GunnerDataBase gunnerDataBase;
    [SerializeField] private GunsDataBase gunsDataBase;

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

    public GAME_MODE gameMode = GAME_MODE.NORMAL;
    public CARD_SETS cardSets = CARD_SETS.SINGLE;
    public List<CARD_POOL> cardPools;
}
