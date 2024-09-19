using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class GunnerDatabase : ScriptableObject
{
    [SerializeField] private List<GunnerData> gunnerDataList = new List<GunnerData>();

    public List<GunnerData> GetGunnerDataList() { return gunnerDataList; }

    public GunnerData SearchGunnerByID(string ID)
    {
        GunnerData result = null;

        foreach (var data in gunnerDataList)
        {
            if (data.GetGunnerId() == ID)
            {
                result = data;
            }
            else
            {
                continue;
            }
        }

        return result;
    }
}
