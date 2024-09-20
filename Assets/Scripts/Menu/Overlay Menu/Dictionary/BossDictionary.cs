using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Fusion.Editor.FusionHubWindow;
using static OverlayMenu;
using static UnityEngine.ParticleSystem;

namespace DictionaryMenu
{
    public class BossDictionary : MonoBehaviour, IDictionaryMenu
    {
        [Header("Prefabs")]
        [SerializeField] private GameObject bossIconPrefab;

        [Header("Locations")]
        [SerializeField] private Transform iconLocation;
        [SerializeField] private BossDetails detailsDisplay;

        private bool first = false;

        public Button TopIcon {  get; private set; }
        public static BossDetails DetailsDisplay { get; private set; }

        private void Awake()
        {
            DetailsDisplay = detailsDisplay;
        }

        public void Initialize()
        {
            first = true;
            if (iconLocation.childCount != 0)
            {
                DestroyAllChildren(iconLocation);
            }
            foreach (var cardpool in DictionaryManager.Cardpools)
            {
                foreach(var data in cardpool.Bosses)
                {
                    BossIcon bossIcon = Instantiate(bossIconPrefab, iconLocation).GetComponent<BossIcon>();
                    bossIcon.SetIconData(data);
                    if (first)
                    {
                        TopIcon = bossIcon.DetailsButton;
                        first = false;
                    }
                    bossIcon.gameObject.layer = 7;
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