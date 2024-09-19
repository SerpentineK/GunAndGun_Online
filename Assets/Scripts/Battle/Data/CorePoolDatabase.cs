using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "CorePoolXX_PoolName", menuName = "Create Core Cardpool")]
public class CorePoolDatabase : CardpoolDatabase
{
    [SerializeField] private ExpansionPoolDatabase expansion;

    public ExpansionPoolDatabase MyExpansion { get { return expansion; } }
}
