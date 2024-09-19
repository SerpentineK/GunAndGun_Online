using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static OverlayMenu;

namespace DictionaryMenu
{
    public class GunDictionary : MonoBehaviour, IDictionaryMenu
    {
        public static GunDictionary Instance { get; private set; }

        [Header("Parents")]
        [SerializeField] private Transform showcaseParent;
        [SerializeField] private Transform switcherParent;

        [Header("Prefabs")]
        [SerializeField] private GameObject showcasePrefab;
        [SerializeField] private GameObject marginObjectPrefab;
        [SerializeField] private GameObject switcherPrefab;

        [Header("Scroll View")]
        [SerializeField] private ScrollRect scrollView;

        [Header("Details")]
        [SerializeField] private GunDetails details;

        public static Vector3 ViewCenter { get; private set; } = new(0f, -140f, 0f);
        public static float IconDenominator { get; private set; } = 1000f;

        private List<GunShowcase> showcaseList;
        private List<GunPoolSwitcher> switcherList;

        private readonly Vector2 frontMarginSize = new(275f, 750f);
        private readonly Vector2 rearMarginSize = new(625f, 750f);

        private GunShowcase currentShowcase = null;

        public void Awake()
        {
            Instance = this;
        }


        public void Initialize()
        {
            if (showcaseParent.childCount != 0)
            {
                DestroyAllChildren(showcaseParent);
            }

            if (switcherParent.childCount != 0)
            {
                DestroyAllChildren(switcherParent);
            }

            showcaseList = new();
            switcherList = new();

            GameObject frontMargin = Instantiate(marginObjectPrefab, showcaseParent);
            frontMargin.name = "Front Margin";
            frontMargin.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, frontMarginSize.x);

            foreach (var cardpool in DictionaryManager.Cardpools) 
            {
                GunShowcase showcase = Instantiate(showcasePrefab, showcaseParent).GetComponent<GunShowcase>();
                GunPoolSwitcher switcher = Instantiate(switcherPrefab, switcherParent).GetComponent<GunPoolSwitcher>();

                showcase.name = cardpool.CardpoolNameENG + " NAGUN Showcase";
                switcher.name = cardpool.CardpoolNameENG + " switcher";

                showcaseList.Add(showcase);
                switcherList.Add(switcher);

                showcase.SetGunShowcase(cardpool.GunsDatabase);
                switcher.SetSwitcher(cardpool, showcase);
            }

            GameObject rearMargin = Instantiate(marginObjectPrefab, showcaseParent);
            rearMargin.name = "Rear Margin";
            rearMargin.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rearMarginSize.x);

            switcherList[0].GetComponent<Button>().onClick.Invoke();
            ToggleGameObject(details.gameObject, false);
        }

        public void SwitchShowcase(GunShowcase nextShowcase)
        {
            if((nextShowcase == null)||(currentShowcase == nextShowcase))
            {
                return;
            }

            scrollView.onValueChanged.RemoveAllListeners();

            foreach (var showcase in showcaseList)
            {
                if (showcase == nextShowcase)
                {
                    ToggleGameObject(showcase.gameObject, true);
                }
                else
                {
                    ToggleGameObject(showcase.gameObject, false);
                }
            }

            GunIcon[] nextIcons = nextShowcase.GetComponentsInChildren<GunIcon>();
            foreach (var icon in nextIcons)
            {
                icon.ForceSetIconSize();
            }

            scrollView.onValueChanged.AddListener(nextShowcase.UpdateIcons);

            currentShowcase = nextShowcase;

            SnapTo(nextIcons[0].transform);
        }


        public void SnapTo(Transform target)
        {
            scrollView.onValueChanged.RemoveAllListeners();

            Canvas.ForceUpdateCanvases();

            RectTransform showcaseParentRect = showcaseParent.GetComponent<RectTransform>();
                showcaseParentRect.localPosition =
                        (Vector2)scrollView.transform.InverseTransformPoint(showcaseParentRect.position)
                        - (Vector2)scrollView.transform.InverseTransformPoint(target.position)
                        + (Vector2)scrollView.transform.InverseTransformPoint(ViewCenter);

            currentShowcase.GetComponent<GunShowcase>().UpdateIcons(new(0,0));

            scrollView.onValueChanged.AddListener(currentShowcase.UpdateIcons);
        }

        public void ShowDetails(GunIcon icon)
        {
            ToggleGameObject(details.gameObject, true);
            details.SetDetails(icon.MyData);
        }

        public void HideDetails()
        {
            ToggleGameObject(details.gameObject, false);
        }

        public void EnterDictMenu()
        {
            ToggleGameObject(gameObject, true);
            if (currentShowcase != null) { SwitchShowcase(currentShowcase); }
            HideDetails();
        }

        public void ExitDictMenu()
        {
            HideDetails();
            ToggleGameObject(gameObject,false);
        }
    }
}