using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObject : MonoBehaviour
{
    public bool isActive;
    public bool isSelected;
    public GameObject candidateObject;
    public BoxCollider2D selectionCollider;
    public SelectionReciever reciever;
    public SpriteMask mask;

    public void Awake()
    {
        isActive = false;
        selectionCollider.enabled = false;
        reciever = this.gameObject.AddComponent<SelectionReciever>();
        reciever.enabled = false;
    }

    public void ActivateSelectable()
    {
        mask.enabled = true;
        selectionCollider.enabled = true;
        isActive = true;
        reciever.enabled = true;
    }
}
