using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    // �������̊D�F�I�[�o�[���C��`�悷��Renderer
    [SerializeField] private GameObject greyCurtain;

    public int numberOfSelection;
    public SelectableObject[] selectables;
    public List<SelectableObject> selected;
}
