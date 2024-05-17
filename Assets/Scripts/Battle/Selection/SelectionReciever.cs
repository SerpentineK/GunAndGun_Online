using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionReciever : MonoBehaviour, IPointerClickHandler
{
    public SelectableObject myObject;

    public void OnPointerClick(PointerEventData eventData)
    { 
        myObject.isSelected = !myObject.isSelected;
    }
}
