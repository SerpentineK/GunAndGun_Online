using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static OverlayMenu;

namespace DictionaryMenu
{
    public class GunnerDictionary : MonoBehaviour, IDictionaryMenu
    {
        [Header("Prefabs")]
        [SerializeField] private GameObject gunnerIconPrefab;
        [SerializeField] private GameObject gunnerIconParentPrefab;

        [Header("Locations")]
        [SerializeField] private Transform iconLocation;
        [SerializeField] private GunnerDetails detailsDisplay;

        public static GunnerDetails DetailsDisplay { get; private set; }
        public Button TopIcon { get; private set; }

        public void Awake()
        {
            DetailsDisplay = detailsDisplay;
        }

        public void Initialize()
        {
            bool first = true;
            foreach (var cardpool in DictionaryManager.Cardpools)
            {
                foreach (var data in cardpool.GunnerDatabase.GetGunnerDataList())
                {
                    GunnerIcon icon = Instantiate(gunnerIconPrefab, iconLocation).GetComponent<GunnerIcon>();
                    icon.SetIconContent(data);
                    if (first)
                    {
                        TopIcon = icon.GetComponent<Button>();
                        first = false;
                    }
                    icon.gameObject.layer = 7;
                }
            }
            TopIcon.onClick.Invoke();
        }
        public void EnterDictMenu()
        {
            ToggleGameObject(gameObject, true);
        }
        public void ExitDictMenu()
        {
            ToggleGameObject(gameObject, false);
        }
    }
}