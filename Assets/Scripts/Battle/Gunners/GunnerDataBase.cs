using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
[CreateAssetMenu(fileName = "GunnerDataBase", menuName = "CreateGunnerDataBase")]
public class GunnerDataBase : ScriptableObject
{
    [SerializeField] private List<GunnerData> gunnerDataList = new List<GunnerData>();

    public List<GunnerData> GetGunnerDataList() { return gunnerDataList; }
}
