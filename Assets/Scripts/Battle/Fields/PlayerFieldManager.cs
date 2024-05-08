using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFieldManager : FieldManager
{
    public static PlayerFieldManager instance;

    public void Awake()
    {
        instance = this;
    }

}
