using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager instance;

    // 何を選択するのか
    public enum SelectionClassification
    {
        None,
        Card,
        Field,
        Gun,
        Entity
    }

    // どのように選択するのか
    public enum SelectionMethod
    {
        None,
        Automatic_Random, // 自動かつランダム
        Automatic_All, // 自動かつ全選択
        Manual // 手動
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
