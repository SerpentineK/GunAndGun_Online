using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SettingsMenu
{
    public class ControlsSettings : MonoBehaviour, ISettingsMenu
    {
        [SerializeField] private TMP_Dropdown playMethod;
        [SerializeField] private TMP_Dropdown confirmation;
        [SerializeField] private TMP_Dropdown quickSelect;

        public LocalSettings MyLocalSettings { get; set; }

        public void ReflectLocal()
        {
            playMethod.value = (int)MyLocalSettings.cardUsageMethod;

            if (MyLocalSettings.singularCardConfirmation)
            {
                confirmation.value = 0;
            }
            else
            {
                confirmation.value = 1;
            }
            if (MyLocalSettings.selectionByNumberKey)
            {
                quickSelect.value = 0;
            }
            else
            {
                quickSelect.value = 1;
            }
        }

        public void OnValueChange_PlayMethod()
        {
            MyLocalSettings.cardUsageMethod = 
                (LocalSettings.CardUsageMethod)
                Enum.ToObject(typeof(LocalSettings.CardUsageMethod), playMethod.value);
        }

        public void OnValueChange_Confirmation()
        {
            MyLocalSettings.singularCardConfirmation = (confirmation.value == 0);
        }

        public void OnValueChange_QuickSelect()
        {
            MyLocalSettings.selectionByNumberKey = (quickSelect.value == 0);
        }

    }
}