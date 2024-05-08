using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObject : MonoBehaviour
{
    public bool isActive;
    public GameObject candidateObject;
    public BoxCollider2D myCollider;

    public void Awake()
    {
        isActive = false;
        candidateObject = this.gameObject;
        myCollider = GetComponent<BoxCollider2D>();
    }
}
