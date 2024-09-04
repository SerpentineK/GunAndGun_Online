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

    private int masterVolume = 100;
    private int bgmVolume = 100;
    private int sfxVolume = 100; 
    private bool muteAll = false;


    public void AdjustSlider(Slider slider, int value)
    {
        slider.value = value;
    }

    public void AdjustInputField(TMP_InputField field, string value)
    {
        field.text = value;
    }

    public void AdjustMasterVolume(bool isSlider)
    {
        int myInt;

        if (isSlider)
        {
            myInt = (int)masterVolumeSlider.value;
            string myString = myInt.ToString();
            AdjustInputField(masterVolumeInput, myString);
        }
        else
        {
            myInt = int.Parse(masterVolumeInput.text);
            if (myInt > 100)
            {
                myInt = 100;
                masterVolumeInput.text = myInt.ToString();
            }
            AdjustSlider(masterVolumeSlider, myInt);
        }

        masterVolume = myInt;

        UpdateSoundSettings(muteAll, masterVolume, bgmVolume, sfxVolume);
    }

    public void AdjustBGMVolume(bool isSlider)
    {
        int myInt;

        if (isSlider)
        {
            myInt = (int)bgmVolumeSlider.value;
            string myString = myInt.ToString();
            AdjustInputField(bgmVolumeInput, myString);
        }
        else
        {
            myInt = int.Parse(bgmVolumeInput.text);
            if (myInt > 100)
            {
                myInt = 100;
                bgmVolumeInput.text = myInt.ToString();
            }
            AdjustSlider(bgmVolumeSlider, myInt);
        }

        bgmVolume = myInt;

        UpdateSoundSettings(muteAll, masterVolume, bgmVolume, sfxVolume);
    }

    public void AdjustSFXVolume(bool isSlider)
    {
        int myInt;

        if (isSlider)
        {
            myInt = (int)sfxVolumeSlider.value;
            string myString = myInt.ToString();
            AdjustInputField(sfxVolumeInput, myString);
        }
        else
        {
            myInt = int.Parse(sfxVolumeInput.text);
            if (myInt > 100)
            {
                myInt = 100;
                sfxVolumeInput.text = myInt.ToString();
            }
            AdjustSlider(sfxVolumeSlider, myInt);
        }

        sfxVolume = myInt;

        UpdateSoundSettings(muteAll, masterVolume, bgmVolume, sfxVolume);
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

        UpdateSoundSettings(muteAll, masterVolume, bgmVolume, sfxVolume);
    }
}
