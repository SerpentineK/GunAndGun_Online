using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuStateManager : MonoBehaviour
{
    public static MenuStateManager instance;

    public void Awake()
    {
        instance = this;
    }

    public void OnButtonPressed_Battle()
    {

    }

    public void OnButtonPressed_Boss()
    {

    }

    public void OnButtonPressed_Tutorial()
    {

    }
}
