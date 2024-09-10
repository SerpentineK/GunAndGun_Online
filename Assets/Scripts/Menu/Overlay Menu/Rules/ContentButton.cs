using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RulesMenu
{
    public class ContentButton : MonoBehaviour
    {
        [SerializeField] private Sprite descriptionContent;
        [SerializeField] private GameObject highlight;

        public Sprite DescriptionContent { get { return descriptionContent; } }
        public GameObject Highlight { get { return highlight; } }

        public void InputContent()
        {
            RulesMenuManager.SetContent(DescriptionContent);
            RulesMenuManager.HighlightSubIndex(this);
        }

    }
}
