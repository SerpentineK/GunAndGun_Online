using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System;

public class NetworkController : NetworkBehaviour
{
    [SerializeField] private GameObject startDisplay;


    enum GamePhase
    {
        Starting,
        Running,
        Ending
    }
    [Networked] private TickTimer Timer { get; set; }
    [Networked] private GamePhase Phase { get; set; }
    [Networked] private NetworkBehaviourId Winner { get; set; }
    public bool GameIsRunning => Phase == GamePhase.Running;

    private TickTimer _dontCheckforWinTimer;

    private List<NetworkBehaviourId> _playerDataNetworkedIds = new List<NetworkBehaviourId>();

    private static NetworkController _singleton;

    public static NetworkController Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton != null)
            {
                throw new InvalidOperationException();
            }
            _singleton = value;
        }
    }

    private void Awake()
    {
        GetComponent<NetworkObject>().Flags |= NetworkObjectFlags.MasterClientObject;
        Singleton = this;
    }

    private void OnDestroy()
    {
        if (Singleton == this)
        {
            _singleton = null;
        }
        else
        {
            throw new InvalidOperationException();
        }

    }

}
