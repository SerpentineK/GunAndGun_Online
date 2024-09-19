using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "ExpansionPoolXX_PoolName", menuName = "Create Expansion Cardpool")]
public class ExpansionPoolDatabase : CardpoolDatabase
{
    [SerializeField] private CorePoolDatabase core;
    
    public CorePoolDatabase MyCore {  get { return core; } }
}
