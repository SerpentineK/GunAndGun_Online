using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuleIndexButton : MonoBehaviour
{
    [SerializeField] private GameObject subIndex;
    [SerializeField] private GameObject highlight;

    public GameObject SubIndex { get {  return subIndex; } }
    public GameObject Highlight { get { return highlight; } }

    public void ActivateSubIndex()
    {
        RulesMenu.SetSubIndex(SubIndex);
        RulesMenu.HighlightIndex(this);
    }
}
