using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace DictionaryMenu
{
    public class GunnerIcon : MonoBehaviour
    {
        public GunnerData MyData { get; private set; }

        [SerializeField] private Image imageComponent;

        public void SetIconContent(GunnerData data)
        {
            MyData = data;
            imageComponent.sprite = data.GetGunnerThumbnail();
        }

        public void SendGunnerInfo()
        {
            GunnerDictionary.instance.DisplayData(MyData);
        }
    }
}
