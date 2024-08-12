using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager instance;

    // ����I������̂�
    public enum SelectionClassification
    {
        None,
        Card,
        Field,
        Gun,
        Entity
    }

    // �ǂ̂悤�ɑI������̂�
    public enum SelectionMethod
    {
        None,
        Automatic_Random, // �����������_��
        Automatic_All, // �������S�I��
        Manual // �蓮
    }

    [SerializeField] private SelectableObject[] cards;
    [SerializeField] private SelectableObject[] fields;
    [SerializeField] private SelectableObject[] guns;
    [SerializeField] private SelectableObject[] entities;

    [SerializeField] private GameObject selectorPrefab;

    public int selectionNumber;
    
    public void InitiateSelector()
    {
        GameObject selector = Instantiate(selectorPrefab);
    }
}
