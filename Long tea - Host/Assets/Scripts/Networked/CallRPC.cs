using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Events;

public class CallRPC : NetworkBehaviour
{
    [SerializeField] private UnityEvent RPCToCall;

    [ClientRpc]
    public void RPCCallEvent()
    {
        RPCToCall.Invoke();
    }
}
