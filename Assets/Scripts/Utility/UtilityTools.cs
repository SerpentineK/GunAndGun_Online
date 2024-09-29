using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilityTools
{
    public static void ToggleGameObject(GameObject myObject, bool state)
    {
        if (myObject.activeSelf != state) { myObject.SetActive(state); }
    }

    public static void DestroyAllChildren(Transform parentTransform)
    {
        for (int i = 0; i < parentTransform.childCount; i++)
        {
            Object.Destroy(parentTransform.GetChild(i).gameObject);
        }
    }
}
