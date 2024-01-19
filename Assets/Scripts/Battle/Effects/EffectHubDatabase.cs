using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HubDataBase", menuName = "CreateEffectHubDataBase")]
public class EffectHubDatabase : ScriptableObject
{
    [SerializeField] private List<EffectHub> effectHubList = new();

    public List<EffectHub> GetEffectHubList() { return effectHubList; }
}
