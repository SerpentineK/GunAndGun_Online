using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class UtilityTools
{
    public static void ToggleGameObject(GameObject myObject, bool state)
    {
        if (myObject.activeSelf != state) { myObject.SetActive(state); }
    }

    public static void DestroyAllChildren(Transform parentTransform)
    {
        for (int i = 0; i < parentTransform.childCount; i++)
        {
            Object.Destroy(parentTransform.GetChild(i).gameObject);
        }
    }

    public static string CreateRandomNumberString(int length)
    {
        string[] array = new string[length];

        for(int i = 0;i < length; i++)
        {
            array[i] = Random.Range(0, 9).ToString();
        }

        return string.Join(string.Empty, array);
    }

    public static Dictionary<string, string> PhotonRegionDictionary = new()
    {
        { "asia", "Asia" },
        { "au", "Australia" },
        { "cae", "Canada, East" },
        { "cn", "Chinese Mainland" },
        { "eu", "Europe" },
        { "hk", "Hong Kong" },
        { "in", "India" },
        { "jp", "Japan" },
        { "sa", "South America" },
        { "kr", "South Korea" },
        { "tr", "Turkey" },
        { "uae", "United Arab Emirates" },
        { "us", "USA, East" },
        { "usw", "USA, West" },
        { "ussc", "USA, South Central" },
    };

    
}
