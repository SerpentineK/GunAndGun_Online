using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Fusion.Editor.FusionHubWindow;


namespace DictionaryMenu
{
    public class GunnerIcon : MonoBehaviour
    {
        public GunnerData MyData { get; private set; }

        [SerializeField] private Image imageComponent;
        [SerializeField] private Image shadowComponent;
        [SerializeField] private TMP_Text nameComponent;

        private readonly Vector3 imagePos = new(-125, 0, 0);
        private readonly Vector3 imageScale = new(0.15f, 0.15f, 0.15f);
        private readonly Vector3 shadowPos = new(0, -100, 0);
        private readonly Vector3 shadowScale = new(0.3f, 0.3f, 0.3f);

        private readonly int selectedFontSize = 100;
        private readonly int dormantFontSize = 80;

        public void SetIconContent(GunnerData data)
        {
            MyData = data;

            SetScaleAndPosition(MyData, imageComponent, imageScale, imagePos);
            SetScaleAndPosition(MyData, shadowComponent, shadowScale, shadowPos);

            ToggleSelectionDisplay(false);

            nameComponent.SetText(data.GetGunnerName());
            name = data.GetGunnerName();
        }

        public void SetScaleAndPosition(GunnerData data, Image myComponent, Vector3 defScale, Vector3 defPos)
        {
            myComponent.sprite = data.GetGunnerImage();
            myComponent.SetNativeSize();
            RectTransform myRect = myComponent.GetComponent<RectTransform>();
            myRect.localScale = defScale;
            Vector3 myPosData = data.GetIconVector();
            myPosData /= 10;
            myRect.anchoredPosition = myPosData + defPos;
        }

        public void SendGunnerInfo()
        {
            GunnerDictionary.DetailsDisplay.SetDetails(MyData);
        }

        public void SignifyMySelection()
        {
            GunnerIcon[] icons = GunnerDictionary.IconLocation.GetComponentsInChildren<GunnerIcon>();
            foreach (var icon in icons)
            {
                if(icon != this)
                {
                    icon.ToggleSelectionDisplay(false);
                }
                else
                {
                    icon.ToggleSelectionDisplay(true);
                }
            }
        }

        public void ToggleSelectionDisplay(bool state)
        {
            if (!state)
            {
                nameComponent.fontSize = dormantFontSize;
                nameComponent.fontStyle = FontStyles.Normal;
                OverlayMenu.ToggleGameObject(shadowComponent.gameObject, false);
            }
            else
            {
                nameComponent.fontSize = selectedFontSize;
                nameComponent.fontStyle = FontStyles.Underline;
                OverlayMenu.ToggleGameObject(shadowComponent.gameObject, true);
            }
        }
    }
}
