using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static PlayerSettings;

public class ControlsSettings : MonoBehaviour
{

    public void OnValueChange_PlayMethod(TMP_Dropdown dropdown)
    {
        cardUsageMethod = (CardUsageMethod)Enum.ToObject(typeof(CardUsageMethod), dropdown.value);
    }

    public void OnValueChange_Confirmation(TMP_Dropdown dropdown)
    {
        singularCardConfirmation = (dropdown.value == 0);
    }

    public void OnValueChange_QuickSelect(TMP_Dropdown dropdown)
    {
        selectionByNumberKey = (dropdown.value == 0);
    }
}
