using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RulesMenu
{
    public class IndexButton : MonoBehaviour
    {
        [SerializeField] private GameObject subIndex;
        [SerializeField] private GameObject highlight;
        [SerializeField] private TMP_Text label;

        public GameObject SubIndex { get { return subIndex; } }
        public GameObject Highlight { get { return highlight; } }
        public TMP_Text Label { get { return label; } }

        public void ActivateSubIndex()
        {
            RulesMenuManager.SetSubIndex(SubIndex);
            RulesMenuManager.HighlightIndex(this);
        }
    }
}