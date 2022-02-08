using Mirror;
using UnityEngine;
using TMPro;

public class NetworkShortcuts : MonoBehaviour
{
    [SerializeField] private TMP_InputField ipField;

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

    public void StartHost()
    {
        NetworkManager.singleton.StartHost();
    }

    public void StartClient()
    {
        NetworkManager.singleton.StartClient();
    }
}
