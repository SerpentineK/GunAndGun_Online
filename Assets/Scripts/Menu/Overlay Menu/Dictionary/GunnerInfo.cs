using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static OverlayMenu;

namespace DictionaryMenu
{
    public class GunnerInfo : MonoBehaviour
    {
        private GunnerData currentData = null;

        [SerializeField] private Image imageArea;
        [SerializeField] private TMP_Text nameArea;
        [SerializeField] private TMP_Text speedArea;
        [SerializeField] private TMP_Text handArea;
        [SerializeField] private TMP_Text abilityArea;

        [SerializeField] private GunnerDetails details;

        public void SetInfoContent(GunnerData data)
        {
            imageArea.sprite = data.GetGunnerImage();
            nameArea.SetText(data.GetGunnerName());
            speedArea.SetText(string.Format("‘¬“x: {0:00}", data.GetGunnerAgility()));
            data.UpdateHandValue();
            handArea.SetText(string.Format("ŽèŽD: {0}", data.gunnerHandValue.MyStringValue));
            abilityArea.SetText(data.GetGunnerAbility());

            currentData = data;
        }

        public void ShowDetails()
        {
            details.SetDetails(currentData);
            ToggleGameObject(details.gameObject, true);
        }

        public void ExitDetails()
        {
            ToggleGameObject(details.gameObject, false);
        }
    }
}