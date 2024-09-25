using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SettingsMenu
{
    public class GraphicsSettings : MonoBehaviour, ISettingsMenu
    {
        [SerializeField] private TMP_Dropdown aspect;
        [SerializeField] private TMP_Dropdown framerate;
        [SerializeField] private Toggle vsync;

        public LocalSettings MyLocalSettings { get; set; }

        public void ReflectLocal()
        {
            aspect.value = Array.IndexOf(MyLocalSettings.aspectRatioArray, MyLocalSettings.AspectRatio);
            framerate.value = Array.IndexOf(MyLocalSettings.maxFramerateArray, MyLocalSettings.MaxFramerate);
            vsync.isOn = MyLocalSettings.vsyncEnabled;
        }

        public void OnValueChange_AspectRatio()
        {
            MyLocalSettings.AspectRatio = MyLocalSettings.aspectRatioArray[aspect.value];
        }

        public void OnValueChange_Framerate()
        {
            MyLocalSettings.MaxFramerate = MyLocalSettings.maxFramerateArray[framerate.value];
        }

        public void OnValueChange_VSync()
        {
            MyLocalSettings.vsyncEnabled = vsync.isOn;
        }
    }
}