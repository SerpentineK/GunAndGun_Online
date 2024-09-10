using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static PlayerSettings;

public class MiscellaneousSettings : MonoBehaviour
{
    public void OnValueChange_EffectsHandling(TMP_Dropdown dropdown)
    {
        specialEffectHandling = (SpecialEffectHandling)Enum.ToObject(typeof(SpecialEffectHandling), dropdown.value);
    }

    public void OnValueChange_InfoDisplay(TMP_Dropdown dropdown)
    {
        informationDisplay = (InformationDisplay)Enum.ToObject(typeof(InformationDisplay), dropdown.value);
    }
}
