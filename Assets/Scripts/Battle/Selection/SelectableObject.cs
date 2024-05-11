using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObject : MonoBehaviour
{
    public bool isActive;
    public GameObject candidateObject;
    public BoxCollider2D selectionCollider;
    public SpriteMask mask;

    public void Awake()
    {
        isActive = false;
        selectionCollider.enabled = false;
    }

    public void ActivateSelectable()
    {
        mask.enabled = true;
        selectionCollider.enabled = true;
        isActive = true;
    }
}
