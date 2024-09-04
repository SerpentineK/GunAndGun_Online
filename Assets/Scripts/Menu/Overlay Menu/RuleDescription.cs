using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleDescription : ScriptableObject
{
    [SerializeField] private string myTitle;
    [SerializeField, TextArea(20, 50)] private string myText;

}
