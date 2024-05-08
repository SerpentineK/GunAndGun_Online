using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager instance;

    // �������̊D�F�I�[�o�[���C��`�悷��Renderer
    [SerializeField] private SpriteRenderer overlayRenderer;

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

    public void PrepareForSelection(SelectionClassification classification)
    {

    }
}
