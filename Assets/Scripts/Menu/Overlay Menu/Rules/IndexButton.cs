using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RulesMenu
{
    public class IndexButton : MonoBehaviour
    {
        [SerializeField] private GameObject subIndex;
        [SerializeField] private GameObject highlight;

        public GameObject SubIndex { get { return subIndex; } }
        public GameObject Highlight { get { return highlight; } }

        public void ActivateSubIndex()
        {
            RulesMenuManager.SetSubIndex(SubIndex);
            RulesMenuManager.HighlightIndex(this);
        }
    }
}