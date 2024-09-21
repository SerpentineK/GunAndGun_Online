using DictionaryMenu;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// ゲーム本体にオーバーレイ表示されるメニュー画面
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
    // 現在表示されているメニューの深度と現在表示されているMenuGameObjectを表す
    private int depth;
    private GameObject currentMenu;

    // 特定のGameObjectをactivateしたりdeactivateしたりする際いちいちactiveSelf確認するのが面倒だったから関数化した
    public static void ToggleGameObject(GameObject myObject, bool state)
    {
        if (myObject.activeSelf != state) { myObject.SetActive(state); }
    }

    // 同じく、子オブジェクト全削除をいちいち書くのが面倒だったから関数化
    public static void DestroyAllChildren(Transform transform)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    // 同じく、layoutgroup更新用関数
    public static void ForceHorizontalLayout(LayoutGroup layoutGroup)
    {
        layoutGroup.CalculateLayoutInputHorizontal();
        layoutGroup.CalculateLayoutInputVertical();
        layoutGroup.SetLayoutHorizontal();
        layoutGroup.SetLayoutVertical();
        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(layoutGroup.GetComponent<RectTransform>());
    }

    // 一度Awake()を呼び出すためにactivateし、その後すぐにdeactivateする関数
    public static void TeaseGameObject(GameObject myObject)
    {
        ToggleGameObject(myObject, true);
        ToggleGameObject(myObject, false);
    }

    // 読み込み時にメニュー本体をDeactivate、メニュー表示用ボタンだけActivateする
    public void Awake()
    {
        ToggleGameObject(menuOutline, false);
        ToggleGameObject(activateButton, true);
    }

    // メニュー表示用ボタンが押された時の処理
    public void OnButtonPressed_Activate()
    {
        // メニュー本体をActivate、表示ボタンと「戻る」ボタンをDeactivate
        ToggleGameObject(menuOutline, true);
        ToggleGameObject(activateButton, false);
        ToggleGameObject(returnButton, false);

        depth = 0;
        ChangeMenu(topMenu);
    }

    public void OnButtonPressed_Exit()
    {
        ToggleGameObject(menuOutline, false);
        ToggleGameObject(activateButton, true);
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
        dictionaryMenu.GetComponent<DictionaryManager>().InitializeDictionaries();
    }

    public void OnButtonPressed_Credits()
    {
        depth = 1;
        ChangeMenu(creditsMenu);
    }

    public void ChangeMenu(GameObject nextMenu)
    {
        if(currentMenu == nextMenu) { return; }
        
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
