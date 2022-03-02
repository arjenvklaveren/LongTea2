using Mirror.Discovery;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class ShowServers : MonoBehaviour
{
    [Header("List settings")]
    [SerializeField] private Transform listParent;
    [SerializeField] private GameObject listItemPrefab;
    [SerializeField] private UnityEvent listenersToAddToButton;

    [Header("Discovery settings")]
    readonly Dictionary<long, ServerResponse> discoveredServers = new Dictionary<long, ServerResponse>();
    public NetworkDiscovery networkDiscovery;

    public void RefreshServerList()
    {
        discoveredServers.Clear();
        networkDiscovery.StartDiscovery();
        StartCoroutine(DrawServerList());
    }

    IEnumerator DrawServerList()
    {
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Clearing serverlist");
        foreach(Transform serverListItem in listParent)
        {
            Destroy(serverListItem.gameObject);
        }
        Debug.Log($"Found {discoveredServers.Count} servers");
        Debug.Log("Drawing serverlist");
        foreach (ServerResponse info in discoveredServers.Values)
        {
            SetSlot(info);
            Debug.Log($"Found server {info.EndPoint.Address.ToString()}");
        }
    }

    void Connect(ServerResponse info)
    {
        networkDiscovery.StopDiscovery();
        NetworkManager.singleton.networkAddress = info.EndPoint.Address.ToString();
        NetworkManager.singleton.StartClient();
        //NetworkManager.singleton.StartClient(info.uri);
    }

    void SetSlot(ServerResponse info)
    {
        if(listItemPrefab != null)
        {
            GameObject serverListItem = Instantiate(listItemPrefab, listParent);

            serverListItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(info.EndPoint.Address.ToString());
            //serverListItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(info.availableSlots.ToString());

            serverListItem.GetComponent<Button>().onClick.AddListener(delegate { listenersToAddToButton.Invoke(); });
            serverListItem.GetComponent<EventAfterTime>().timedEvent.AddListener(() => Connect(info));
        }
    }

    public void OnDiscoveredServer(ServerResponse info)
    {
        // Note that you can check the versioning to decide if you can connect to the server or not using this method
        discoveredServers[info.serverId] = info;
        Debug.Log($"Found server {info.serverId}");
    }
}
