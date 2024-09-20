using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static OverlayMenu;

namespace DictionaryMenu
{
    public class GunIcon : MonoBehaviour
    {
        public GunsData MyData { get; private set; }

        [SerializeField] private Image gunsImage;
        [SerializeField] private RectTransform imageRect;
        [SerializeField] private GameObject targetedDisplay;
        [SerializeField] private Image gunsLabel;
        [SerializeField] private Button detailsButton;
        
        private RectTransform myRect;
        private Button myButton;

        private static readonly float minWidth = 150;
        private static readonly float maxWidth = 350;
        private static readonly float grace = 150;

        public void SetIconContent(GunsData data)
        {
            MyData = data;

            gameObject.name = data.name;
            gunsImage.sprite = data.GetGunImage();
            gunsLabel.sprite = data.GetGunLabel();
            myRect = GetComponent<RectTransform>();
            myButton = GetComponent<Button>();
            myButton.onClick.AddListener(SnapToThis);

            detailsButton.onClick.AddListener(ShowMyDetails);
        }

        public void SetIconWidth(float width)
        {
            myRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            imageRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, width);
        }

        public static float DecideSize(float positionX)
        {

            float diff = Math.Abs(positionX - GunDictionary.ViewCenter.x) / GunDictionary.IconDenominator;

            if (diff > 1)
            {
                diff = 1;
            }

            float ratio = 1 - diff;
            float newWidth = (maxWidth - minWidth) * ratio + minWidth;
            return newWidth;
        }

        public void ControlLabelDisplay()
        {
            if (Vector2.Distance(myRect.position, GunDictionary.ViewCenter) <= grace)
            {
                ToggleGameObject(targetedDisplay, true);
            }
            else
            {
                ToggleGameObject(targetedDisplay, false);
            }
        }

        public void ForceSetIconSize()
        {
            float width = DecideSize(transform.position.x);
            ControlLabelDisplay();
            SetIconWidth(width);
        }

        public void SnapToThis()
        {
            GunDictionary.Instance.SnapTo(transform);
        }

        public void ShowMyDetails()
        {
            GunDictionary.Instance.ShowDetails(this);
        }
    }
}