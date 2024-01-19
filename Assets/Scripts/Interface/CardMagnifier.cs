using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CardMagnifier : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject magnifiedCard;

    public void CreateCardForDisplay()
    {
        magnifiedCard = Instantiate(gameObject,gameObject.transform);
        magnifiedCard.GetComponent<CardMagnifier>().enabled = false;
        if (magnifiedCard.activeSelf) { magnifiedCard.SetActive(false); }
        magnifiedCard.transform.SetAsLastSibling();
        magnifiedCard.transform.localScale = Vector3.one*5/2;
        if (magnifiedCard.transform.eulerAngles.z == 180) 
        {
            magnifiedCard.transform.localPosition = new Vector3(0, -260, 0);
            magnifiedCard.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            magnifiedCard.transform.localPosition = new Vector3(0, 260, 0);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gameObject.activeSelf&!magnifiedCard.activeSelf) { magnifiedCard.SetActive(true); }
    }

    public void OnPointerExit(PointerEventData eventData) 
    {
        if(magnifiedCard.activeSelf) { magnifiedCard.SetActive(false);}
    }
    
    // Start is called before the first frame update
    void Start()
    {
        CreateCardForDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
