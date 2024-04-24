using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class GunnerDataBase : ScriptableObject
{
    [SerializeField] private List<GunnerData> gunnerDataList = new List<GunnerData>();

    public List<GunnerData> GetGunnerDataList() { return gunnerDataList; }
}
