using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DictionaryMenu
{
    public class GunnerDictionary : MonoBehaviour
    {
        public static GunnerDictionary instance;

        [Header("Prefabs")]
        [SerializeField] private GameObject gunnerIconPrefab;
        [SerializeField] private GameObject gunnerIconParentPrefab;

        [Header("Locations")]
        [SerializeField] private GameObject iconLocation;
        [SerializeField] private GunnerInfo infoDisplay;

        public void ActivateIcons()
        {
            int iconCount = 0;
            Transform iconParent = null;
            foreach (var database in DictionaryManager.GunnerDataBases)
            {
                foreach (var data in database.GetGunnerDataList())
                {
                    iconCount++;
                    if (iconCount % 4 == 1)
                    {
                        iconParent = Instantiate(gunnerIconParentPrefab, iconLocation.transform).transform;
                    }
                    GunnerIcon icon = Instantiate(gunnerIconPrefab, iconParent).GetComponent<GunnerIcon>();
                    icon.SetIconContent(data);
                    if (iconCount == 1)
                    {
                        icon.SendGunnerInfo();
                    }
                    icon.gameObject.layer = 7;
                }
            }
        }

        public void DisplayData(GunnerData data)
        {
            infoDisplay.SetInfoContent(data);
        }
    }
}