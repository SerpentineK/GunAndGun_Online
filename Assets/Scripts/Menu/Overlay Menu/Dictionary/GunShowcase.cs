using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static OverlayMenu;

namespace DictionaryMenu
{
    public class GunShowcase : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private GameObject iconPrefab;

        [Header("Own Properties")]
        [SerializeField] private RectTransform myRect;

        private List<GunIcon> iconList;
        private Vector3 iconPosition;
        private Quaternion iconRotation;

        public void SetGunShowcase(GunsDatabase database)
        {
            DestroyAllChildren(transform);

            iconList = new();
            iconPosition = new(-1000f, -140f, 0f);
            iconRotation = Quaternion.Euler(0f, 0f, 0f);

            foreach (var data in database.GetGunsDataList()) 
            {
                GunIcon newIcon = Instantiate(iconPrefab, iconPosition, iconRotation, transform).GetComponent<GunIcon>();
                iconPosition.x += 400f;
                iconList.Add(newIcon);
                newIcon.SetIconContent(data);
            }

            ForceHorizontalLayout(gameObject.GetComponent<HorizontalLayoutGroup>());
        }

        public void UpdateIcons(Vector2 myPositionNorm)
        {
            float nextSize = 0f;
            foreach (var icon in iconList)
            {
                icon.ForceSetIconSize();
                nextSize += icon.GetComponent<RectTransform>().rect.size.x;
            }
            myRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, nextSize);
        }

    }
}