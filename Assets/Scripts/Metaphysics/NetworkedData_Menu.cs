using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Metaphysics
{
    public class NetworkedData_Menu : NetworkBehaviour
    {
        [Networked] public string Username { get; set; }
    }
}