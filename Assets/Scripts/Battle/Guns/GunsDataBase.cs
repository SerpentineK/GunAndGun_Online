using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GunsDatabase : ScriptableObject
{
    [SerializeField] private List<GunsData> gunsDataList = new List<GunsData>();
    public List<GunsData> GetGunsDataList() { return gunsDataList; }

    public GunsData SearchGunByID(string ID)
    {
        GunsData result = null;

        foreach (var data in gunsDataList)
        {
            if (data.GetGunId() == ID)
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
