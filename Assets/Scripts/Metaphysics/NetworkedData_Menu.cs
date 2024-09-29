using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Metaphysics
{
    public class NetworkedData_Menu : NetworkBehaviour
    {

        [Networked] public string Username { get; set; }

        [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
        public void RPC_SetUsername(string username)
        {
            Username = username;
            gameObject.name = Username + "_MenuData";
        }
    }
}