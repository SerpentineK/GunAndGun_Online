using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleContentButton : MonoBehaviour
{
    [SerializeField] private Sprite descriptionContent;
    [SerializeField] private GameObject highlight;

    public Sprite DescriptionContent { get { return descriptionContent; } }
    public GameObject Highlight { get { return highlight; } }

    public void InputContent()
    {
        RulesMenu.SetContent(DescriptionContent);
        RulesMenu.HighlightSubIndex(this);
    }

}
