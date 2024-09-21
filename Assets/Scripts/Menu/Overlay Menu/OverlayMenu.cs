using DictionaryMenu;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// �Q�[���{�̂ɃI�[�o�[���C�\������郁�j���[���
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

    // return�{�^�������p�̕ϐ�
    // ���ݕ\������Ă��郁�j���[�̐[�x�ƌ��ݕ\������Ă���MenuGameObject��\��
    private int depth;
    private GameObject currentMenu;

    // �����GameObject��activate������deactivate�����肷��ۂ�������activeSelf�m�F����̂��ʓ|����������֐�������
    public static void ToggleGameObject(GameObject myObject, bool state)
    {
        if (myObject.activeSelf != state) { myObject.SetActive(state); }
    }

    // �������A�q�I�u�W�F�N�g�S�폜���������������̂��ʓ|����������֐���
    public static void DestroyAllChildren(Transform transform)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    // �������Alayoutgroup�X�V�p�֐�
    public static void ForceHorizontalLayout(LayoutGroup layoutGroup)
    {
        layoutGroup.CalculateLayoutInputHorizontal();
        layoutGroup.CalculateLayoutInputVertical();
        layoutGroup.SetLayoutHorizontal();
        layoutGroup.SetLayoutVertical();
        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(layoutGroup.GetComponent<RectTransform>());
    }

    // ��xAwake()���Ăяo�����߂�activate���A���̌シ����deactivate����֐�
    public static void TeaseGameObject(GameObject myObject)
    {
        ToggleGameObject(myObject, true);
        ToggleGameObject(myObject, false);
    }

    // �ǂݍ��ݎ��Ƀ��j���[�{�̂�Deactivate�A���j���[�\���p�{�^������Activate����
    public void Awake()
    {
        ToggleGameObject(menuOutline, false);
        ToggleGameObject(activateButton, true);
    }

    // ���j���[�\���p�{�^���������ꂽ���̏���
    public void OnButtonPressed_Activate()
    {
        // ���j���[�{�̂�Activate�A�\���{�^���Ɓu�߂�v�{�^����Deactivate
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
