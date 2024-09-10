using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DictionaryMenu
{
    public class GunnerDetails : MonoBehaviour
    {
        [SerializeField] private Image detailsImageArea;
        private GunnerData currentData;

        public void SetDetails(GunnerData data)
        {
            detailsImageArea.sprite = data.GetGunnerDetails();
            detailsImageArea.preserveAspect = true;

            float myWidth = detailsImageArea.preferredWidth;
            float myHeight = detailsImageArea.preferredHeight;

            float ratio = 1500 / myWidth;
            RectTransform myRect = detailsImageArea.GetComponent<RectTransform>();
            myRect.sizeDelta = new Vector2(1500, myHeight * ratio);
            myRect.localPosition = new Vector2(0, 0);
        }
    }
}