using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static OverlayMenu;

public class RulesMenu : MonoBehaviour
{
    public static RulesMenu instance;

    [SerializeField] private GameObject[] _indexArray;

    [SerializeField] private GameObject[] _subIndexArray;

    private static GameObject[] indexArray;
    private static GameObject[] subIndexArray;

    [SerializeField] private Image contentRenderer;

    public static Image ContentRenderer { get; private set; }

    [SerializeField] private Button topIndexButton;
    [SerializeField] private Button topContentButton;

    private void Awake()
    {
        instance = this;
        
        indexArray = _indexArray;
        subIndexArray = _subIndexArray;
        
        ContentRenderer = contentRenderer;

        topIndexButton.onClick.Invoke();
        topContentButton.onClick.Invoke();
    }

    

    public static void SetSubIndex(GameObject nextSub)
    {
        foreach (var subIndex in subIndexArray)
        {
            ToggleGameObject(subIndex, false);
        }

        if (nextSub)
        {
            ToggleGameObject(nextSub, true);
        }

    }

    public static void HighlightSubIndex(RuleContentButton nextButton)
    {
        foreach(GameObject subIndex in subIndexArray)
        {
            RuleContentButton[] myButtonArray = subIndex.GetComponentsInChildren<RuleContentButton>();
            foreach (RuleContentButton button in myButtonArray)
            {
                if (nextButton != button) 
                {
                    ToggleGameObject(button.Highlight, false);
                }
                else
                {
                    ToggleGameObject(button.Highlight, true);
                } 
            }
        }
    }

    public static void SetContent(Sprite description)
    {
        ContentRenderer.sprite = description;
        ContentRenderer.preserveAspect = true;

        float myHeight = ContentRenderer.preferredHeight;
        float myWidth = ContentRenderer.preferredWidth;

        float ratio = 1000 / myWidth;
        RectTransform myRect = ContentRenderer.GetComponent<RectTransform>();
        myRect.sizeDelta = new Vector2(1000, myHeight * ratio);
        myRect.localPosition = new Vector2(0, 0);
    }

    public static void HighlightIndex(RuleIndexButton nextButton)
    {
        foreach (GameObject index in indexArray) 
        {
            RuleIndexButton button = index.GetComponent<RuleIndexButton>();
            if (nextButton != button)
            {
                ToggleGameObject(button.Highlight, false);
            }
            else
            {
                ToggleGameObject(button.Highlight, true);
            }
        }
    }

}
