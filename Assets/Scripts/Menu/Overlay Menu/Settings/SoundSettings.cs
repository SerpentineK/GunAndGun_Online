using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SettingsMenu
{
    public class SoundSettings : MonoBehaviour, ISettingsMenu
    {
        [SerializeField] private Slider masterVolumeSlider;
        [SerializeField] private Slider bgmVolumeSlider;
        [SerializeField] private Slider sfxVolumeSlider;
        [SerializeField] private TMP_InputField masterVolumeInput;
        [SerializeField] private TMP_InputField bgmVolumeInput;
        [SerializeField] private TMP_InputField sfxVolumeInput;
        [SerializeField] private Toggle muteToggle;

        public LocalSettings MyLocalSettings { get; set; }

        public void ReflectLocal()
        {
            AdjustInputField(masterVolumeInput, MyLocalSettings.MasterVolume.ToString());
            AdjustSlider(masterVolumeSlider, MyLocalSettings.MasterVolume);
            
            AdjustInputField(bgmVolumeInput, MyLocalSettings.BgmVolume.ToString());
            AdjustSlider(bgmVolumeSlider, MyLocalSettings.BgmVolume);

            AdjustInputField(sfxVolumeInput, MyLocalSettings.SfxVolume.ToString());
            AdjustSlider(sfxVolumeSlider, MyLocalSettings.SfxVolume);

            muteToggle.isOn = MyLocalSettings.muteAll;
        }

        public void AdjustSlider(Slider slider, int value)
        {
            slider.value = value;
        }

        public void AdjustInputField(TMP_InputField field, string value)
        {
            field.text = value;
        }

        public void AdjustMasterVolume(GameObject myObject)
        {
            if (myObject.TryGetComponent<Slider>(out var sliderComponent))
            {
                MyLocalSettings.MasterVolume = (int)sliderComponent.value;
                AdjustInputField(masterVolumeInput, MyLocalSettings.MasterVolume.ToString());
            }
            else if (myObject.TryGetComponent<TMP_InputField>(out var inputFieldComponent))
            {
                MyLocalSettings.MasterVolume = int.Parse(inputFieldComponent.text);
                AdjustInputField(inputFieldComponent, MyLocalSettings.MasterVolume.ToString());
                AdjustSlider(masterVolumeSlider, MyLocalSettings.MasterVolume);
            }
        }

        public void AdjustBGMVolume(GameObject myObject)
        {
            if (myObject.TryGetComponent<Slider>(out var sliderComponent))
            {
                MyLocalSettings.BgmVolume = (int)sliderComponent.value;
                AdjustInputField(bgmVolumeInput, MyLocalSettings.BgmVolume.ToString());
            }
            else if (myObject.TryGetComponent<TMP_InputField>(out var inputFieldComponent))
            {
                MyLocalSettings.BgmVolume = int.Parse(inputFieldComponent.text);
                AdjustInputField(inputFieldComponent, MyLocalSettings.BgmVolume.ToString());
                AdjustSlider(bgmVolumeSlider, MyLocalSettings.BgmVolume);
            }
        }

        public void AdjustSFXVolume(GameObject myObject)
        {
            if (myObject.TryGetComponent<Slider>(out var sliderComponent))
            {
                MyLocalSettings.SfxVolume = (int)sliderComponent.value;
                AdjustInputField(sfxVolumeInput, MyLocalSettings.SfxVolume.ToString());
            }
            else if (myObject.TryGetComponent<TMP_InputField>(out var inputFieldComponent))
            {
                MyLocalSettings.SfxVolume = int.Parse(inputFieldComponent.text);
                AdjustInputField(inputFieldComponent, MyLocalSettings.SfxVolume.ToString());
                AdjustSlider(sfxVolumeSlider, MyLocalSettings.SfxVolume);
            }
        }

        public void ToggleMute()
        {
            bool muteIsActivated = muteToggle.isOn;

            if (muteIsActivated)
            {
                masterVolumeSlider.interactable = false;
                masterVolumeInput.interactable = false;
                bgmVolumeSlider.interactable = false;
                bgmVolumeInput.interactable = false;
                sfxVolumeSlider.interactable = false;
                sfxVolumeInput.interactable = false;
            }
            else
            {
                masterVolumeSlider.interactable = true;
                masterVolumeInput.interactable = true;
                bgmVolumeSlider.interactable = true;
                bgmVolumeInput.interactable = true;
                sfxVolumeSlider.interactable = true;
                sfxVolumeInput.interactable = true;
            }

            MyLocalSettings.muteAll = muteIsActivated;
        }
    }
}