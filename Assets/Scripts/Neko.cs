using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neko : MonoBehaviour
{
    public Neko(int defaultPower)
    {
        power = defaultPower;
    }

    public int power;
    public int NekoPunch()
    {
        return power;
    }
}
