using Mirror;
using UnityEngine;
using UnityEngine.Events;

public class OnNetworkConnect : NetworkBehaviour
{
    [SerializeField] private UnityEvent ifClient;
    [SerializeField] private UnityEvent ifOther;
    [SerializeField] private UnityEvent ifServer;
    [SerializeField] private UnityEvent ifHost;

    // Start is called before the first frame update
    public override void OnStartClient()
    {
        if (isLocalPlayer)
        {
            ifClient.Invoke();
        }
        else if(!isServer)
        {
            ifOther.Invoke();
        }
    }

    public override void OnStartServer()
    {
        if (isServerOnly)
        {
            ifServer.Invoke();
        }
        else if (isServer)
        {
            ifHost.Invoke();
        }
    }
}
