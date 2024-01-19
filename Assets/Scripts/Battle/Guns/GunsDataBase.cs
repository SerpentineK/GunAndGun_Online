using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GunsDataBase", menuName = "CreateGunsDataBase")]
public class GunsDataBase : ScriptableObject
{
    [SerializeField] private List<GunsData> gunsDataLists = new List<GunsData>();

    public List<GunsData> GetGunsDataLists() { return gunsDataLists; }
}
