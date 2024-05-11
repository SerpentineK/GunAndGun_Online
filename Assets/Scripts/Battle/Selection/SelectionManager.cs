using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager instance;

    public enum SelectionClassification
    {
        None,
        Card,
        Field,
        Gun,
        Entity
    }

    public enum SelectionMethod
    {
        None,
        Automatic_Random,
        Automatic_All,
        Manual
    }

    public int selectionNumber;
    private int selectedNumber;
}
