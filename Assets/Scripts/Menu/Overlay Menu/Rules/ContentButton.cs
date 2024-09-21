using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RulesMenu
{
    public class ContentButton : MonoBehaviour
    {
        [SerializeField] private Sprite descriptionContent;
        [SerializeField] private GameObject highlight;
        [SerializeField] private TMP_Text label;

        public Sprite DescriptionContent { get { return descriptionContent; } }
        public GameObject Highlight { get { return highlight; } }
        public TMP_Text Label { get { return label; } }

        public void InputContent()
        {
            RulesMenuManager.SetContent(DescriptionContent);
            RulesMenuManager.HighlightSubIndex(this);
        }

    }
}
