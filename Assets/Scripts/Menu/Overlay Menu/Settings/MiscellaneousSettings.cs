using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SettingsMenu
{
    public class MiscellaneousSettings : MonoBehaviour, ISettingsMenu
    {
        [SerializeField] private TMP_Dropdown effectsHandling;
        [SerializeField] private TMP_Dropdown infoDisplay;

        public LocalSettings MyLocalSettings { get; set; }

        public void ReflectLocal()
        {
            effectsHandling.value = (int)MyLocalSettings.specialEffectHandling;
            infoDisplay.value = (int)MyLocalSettings.informationDisplay;
        }

        public void OnValueChange_EffectsHandling()
        {
            MyLocalSettings.specialEffectHandling = 
                (LocalSettings.SpecialEffectHandling)
                Enum.ToObject(typeof(LocalSettings.SpecialEffectHandling), effectsHandling.value);
        }

        public void OnValueChange_InfoDisplay()
        {
            MyLocalSettings.informationDisplay = 
                (LocalSettings.InformationDisplay)
                Enum.ToObject(typeof(LocalSettings.InformationDisplay), infoDisplay.value);
        }
    }
}