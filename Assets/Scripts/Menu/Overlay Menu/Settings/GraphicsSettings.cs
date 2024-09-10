using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static PlayerSettings;

public class GraphicsSettings : MonoBehaviour
{
    public void OnValueChange_AspectRatio(TMP_Dropdown dropdown)
    {
        AspectRatio = aspectRatioArray[dropdown.value];
    }

    public void OnValueChange_Framerate(TMP_Dropdown dropdown)
    {
        MaxFramerate = maxFramerateArray[dropdown.value];
    }

    public void OnValueChange_VSync(Toggle toggle)
    {
        vsyncEnabled = toggle.isOn;
    }
}
