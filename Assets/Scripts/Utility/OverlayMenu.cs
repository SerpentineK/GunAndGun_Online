using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OverlayMenu : MonoBehaviour
{
    [SerializeField] private GameObject toggleButton;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject menuButtons;

    public void OnButtonPressed_Toggle()
    {
        if (!menu.activeSelf) { menu.SetActive(true); }
        if (toggleButton.activeSelf) { toggleButton.SetActive(false); }
        InitialMenu.instance.ToggleButtons(false);
    }

    public void OnButtonPressed_Exit()
    {
        if (menu.activeSelf) { menu.SetActive(false); }
        if (!toggleButton.activeSelf) { toggleButton.SetActive(true); }
        InitialMenu.instance.ToggleButtons(true);
    }

    public void OnButtonPressed_Settings()
    {
        
    }

    public void OnButtonPressed_Rules()
    {

    }

    public void OnButtonPressed_Dictionary()
    {

    }
}
