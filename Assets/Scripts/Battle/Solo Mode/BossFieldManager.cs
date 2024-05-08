using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFieldManager : FieldManager
{
    public static BossFieldManager instance;

    public Field bossDeckField;
    public Field bossUsedField;

    public void Awake()
    {
        instance = this;
    }

}
