using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OverlayMenu;

public class RulesMenu : MonoBehaviour
{
    public static RulesMenu instance;

    [SerializeField] private GameObject subIndex02;
    [SerializeField] private GameObject subIndex03;
    [SerializeField] private GameObject subIndex04;
    [SerializeField] private GameObject subIndex05;

    private GameObject[] subIndexArray;

    private GameObject currentContent;

    private void Awake()
    {
        instance = this;
        subIndexArray = new GameObject[] { subIndex02, subIndex03, subIndex04, subIndex05 };
    }

    public void SetSubIndex(GameObject nextSub)
    {
        foreach (var subIndex in subIndexArray)
        {
            ToggleGameObject(subIndex, false);
        }

        if (nextSub != null)
        {
            ToggleGameObject(nextSub, true);
        }
    }

    public void SetContent()
    {

    }

    public void OnPressedButton_Index(int index)
    {
        switch(index)
        {
            case 0:
                return;
            case 1:
                SetSubIndex(null);
                break;
        }
    }
}
