using Mirror;
using UnityEngine;
using TMPro;
using Mirror.Discovery;
using Mirror;

public class NetworkShortcuts : MonoBehaviour
{
    [SerializeField] private TMP_InputField ipField;
    [SerializeField] private NetworkDiscovery networkDiscovery;
    [SerializeField] private NetworkRoomManager networkRoomManager;

    private void Start()
    {
        if(ipField != null)
        {
            ipField.text = NetworkManager.singleton.networkAddress;
        }
    }

    public void SetIPToConnect()
    {
        if (!string.IsNullOrEmpty(ipField.text))
        {
            NetworkManager.singleton.networkAddress = ipField.text;
        }
    }

    public void StartClientWithIP()
    {
        SetIPToConnect();
        StartClient();
    }

    public void StartServer()
    {
        NetworkManager.singleton.StartServer();
    }

    public void StartAndOpenForPublic()
    {
        if (networkDiscovery != null)
        {
            //networkDiscoveryHUD.discoveredServers.Clear();
            NetworkManager.singleton.StartHost();
            networkDiscovery.AdvertiseServer();
        }
    }

    public void StartGame()
    {
        if(networkRoomManager == null)
        {
            networkRoomManager = (NetworkRoomManager)NetworkRoomManager.singleton;
        }

        networkRoomManager.OnRoomServerPlayersReady();
    }

    public void StartHost()
    {
        NetworkManager.singleton.StartHost();
    }

    public void StartClient()
    {
        NetworkManager.singleton.StartClient();
    }
}
