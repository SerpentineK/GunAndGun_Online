using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static PlayerSettings;

public class SoundSettings : MonoBehaviour
{
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider bgmVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private TMP_InputField masterVolumeInput;
    [SerializeField] private TMP_InputField bgmVolumeInput;
    [SerializeField] private TMP_InputField sfxVolumeInput;
    [SerializeField] private Toggle muteToggle;


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
            MasterVolume = (int)sliderComponent.value;
            AdjustInputField(masterVolumeInput, MasterVolume.ToString());
        }
        else if (myObject.TryGetComponent<TMP_InputField>(out var inputFieldComponent))
        {
            MasterVolume = int.Parse(inputFieldComponent.text);
            AdjustInputField(inputFieldComponent, MasterVolume.ToString());
            AdjustSlider(masterVolumeSlider, MasterVolume);
        }
    }

    public void AdjustBGMVolume(GameObject myObject)
    {
        if (myObject.TryGetComponent<Slider>(out var sliderComponent))
        {
            BgmVolume = (int)sliderComponent.value;
            AdjustInputField(bgmVolumeInput, BgmVolume.ToString());
        }
        else if (myObject.TryGetComponent<TMP_InputField>(out var inputFieldComponent))
        {
            BgmVolume = int.Parse(inputFieldComponent.text);
            AdjustInputField(inputFieldComponent, BgmVolume.ToString());
            AdjustSlider(bgmVolumeSlider, BgmVolume);
        }
    }

    public void AdjustSFXVolume(GameObject myObject)
    {
        if (myObject.TryGetComponent<Slider>(out var sliderComponent))
        {
            SfxVolume = (int)sliderComponent.value;
            AdjustInputField(sfxVolumeInput, SfxVolume.ToString());
        }
        else if (myObject.TryGetComponent<TMP_InputField>(out var inputFieldComponent))
        {
            SfxVolume = int.Parse(inputFieldComponent.text);
            AdjustInputField(inputFieldComponent, SfxVolume.ToString());
            AdjustSlider(sfxVolumeSlider, SfxVolume);
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

        muteAll = muteIsActivated;
    }
}
