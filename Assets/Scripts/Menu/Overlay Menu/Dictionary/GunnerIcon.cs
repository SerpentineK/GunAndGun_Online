using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace DictionaryMenu
{
    public class GunnerIcon : MonoBehaviour
    {
        public GunnerData MyData { get; private set; }

        [SerializeField] private Image imageComponent;
        [SerializeField] private TMP_Text nameComponent;

        private readonly Vector3 defaultPos = new(-100, 0, 0);
        private readonly Vector3 defaultScale = new(0.15f, 0.15f, 0.15f);

        public void SetIconContent(GunnerData data)
        {
            MyData = data;
            
            imageComponent.sprite = data.GetGunnerImage();
            imageComponent.SetNativeSize();
            RectTransform imageRect = imageComponent.GetComponent<RectTransform>();
            imageRect.localScale = defaultScale;
            Vector3 imagePosData = data.GetIconVector();
            imagePosData /= 10;
            imagePosData += defaultPos;
            imageRect.anchoredPosition = imagePosData;

            nameComponent.SetText(data.GetGunnerName());
        }

        public void SendGunnerInfo()
        {
            GunnerDictionary.DetailsDisplay.SetDetails(MyData);
        }
    }
}
