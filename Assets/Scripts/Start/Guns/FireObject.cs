using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FireObject : MonoBehaviour
{
    public int bulletsRequired;
    public string effectOfFire;
    
    [SerializeField] private GameObject[] bulletIcons;
    [SerializeField] private TMP_Text effectText;

    public void InitializeFireObject()
    {
        if (!gameObject.activeSelf) { gameObject.SetActive(true); }
        for (int i = 0; i < bulletsRequired; i++)
        {
            if (!bulletIcons[i].activeSelf) { bulletIcons[i].SetActive(true); }
        }
        effectText.SetText(effectOfFire);
    }
}
