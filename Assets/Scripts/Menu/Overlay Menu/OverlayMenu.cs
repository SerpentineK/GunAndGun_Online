using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OverlayMenu : MonoBehaviour
{
    [SerializeField] private GameObject activateButton;
    [SerializeField] private GameObject menuOutline;
    [SerializeField] private GameObject topMenu;
    [SerializeField] private GameObject returnButton;

    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject rulesMenu;
    [SerializeField] private GameObject dictionaryMenu;
    [SerializeField] private GameObject creditsMenu;

    // returnボタン実装用の変数
    // 現在表示されているメニューの深度と種別、そして現在表示されているMenuGameObjectを表す
    enum MenuType 
    {
        None,
        Settings,
        Rules,
        Dictionary,
        Credits
    }

    private int depth;
    private GameObject currentMenu;

    public static void ToggleGameObject(GameObject myObject, bool state)
    {
        if (myObject.activeSelf != state) { myObject.SetActive(state); }
    }

    public void Awake()
    {
        ToggleGameObject(menuOutline, false);
        ToggleGameObject(activateButton, true);
    }

    public void OnButtonPressed_Activate()
    {
        ToggleGameObject(menuOutline, true);
        ToggleGameObject(activateButton, false);
        ToggleGameObject(returnButton, false);
        InitialMenu.instance.ToggleButtons(false);
        depth = 0;
        ChangeMenu(topMenu);
    }

    public void OnButtonPressed_Exit()
    {
        ToggleGameObject(menuOutline, false);
        ToggleGameObject(activateButton, true);
        InitialMenu.instance.ToggleButtons(true);
    }

    public void OnButtonPressed_Settings()
    {
        depth = 1;
        ChangeMenu(settingsMenu);
    }

    public void OnButtonPressed_Rules()
    {
        depth = 1;
        ChangeMenu(rulesMenu);
    }

    public void OnButtonPressed_Dictionary()
    {
        depth = 1;
        ChangeMenu(dictionaryMenu);
    }

    public void OnButtonPressed_Credits()
    {
        depth = 1;
        ChangeMenu(creditsMenu);
    }

    public void ChangeMenu(GameObject nextMenu)
    {
        ToggleGameObject(topMenu, false);
        ToggleGameObject(settingsMenu, false);
        ToggleGameObject(rulesMenu, false);
        ToggleGameObject(dictionaryMenu, false);
        ToggleGameObject(creditsMenu, false);

        ToggleGameObject(nextMenu, true);

        currentMenu = nextMenu;

        if (nextMenu == topMenu)
        {
            ToggleGameObject(returnButton, false);
        }
        else
        {
            ToggleGameObject(returnButton, true);
        }
    }

    public void OnButtonPressed_Return()
    {
        switch (depth) 
        {
            case 0:
                return;
            case 1:
                ChangeMenu(topMenu);
                return;
            /*
            case 2:
                switch (type)
                {
                    case MenuType.None:
                        return;
                    case MenuType.Settings:
                        ChangeMenu(settingsMenu);
                        return;
                        case MenuType.Rules:
                        ChangeMenu(rulesMenu);
                        return;
                    case MenuType.Dictionary:
                        ChangeMenu(dictionaryMenu);
                        return;
                    case MenuType.Credits:
                        ChangeMenu(creditsMenu);
                        return;
                }
                return;
            */
        }
    }
}
