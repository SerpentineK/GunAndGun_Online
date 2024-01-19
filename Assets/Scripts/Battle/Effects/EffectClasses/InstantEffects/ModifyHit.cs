using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyHit : InstantEffect
{
    public enum MethodOfModify
    {
        None,
        Add,
        Multiply,
        Divide
    }

    public MethodOfModify method;
    public int modifyer;
}