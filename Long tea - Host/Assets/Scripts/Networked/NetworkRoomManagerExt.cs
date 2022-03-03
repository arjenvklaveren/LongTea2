using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class NetworkRoomManagerExt : NetworkRoomManager
{
    [SerializeField] private GameObject UIPrefab;
    [SerializeField] private string playerListTransformName = "Playerlist";

    private Transform playerListTransform;
    private List<NetworkRoomPlayer> playersInRoom;

    private void Start()
    {
        base.Start();
        InvokeRepeating("TryUpdatePlayerUI", 0.1f, 1.5f);
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);
        StartCoroutine(UpdateRoomPlayerList());
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);
        StartCoroutine(UpdateRoomPlayerList());
    }

    public override void OnClientConnect()
    {
        base.OnClientConnect();
    }


    public override void OnClientDisconnect()
    {
        base.OnClientDisconnect();
        StartCoroutine(UpdateRoomPlayerList());
    }

    public void TryUpdatePlayerUI()
    {
        StartCoroutine(UpdateRoomPlayerList());
    }

    public IEnumerator UpdateRoomPlayerList()
    {
        yield return new WaitForSeconds(0.2f);

        if (!IsSceneActive(RoomScene))
            yield break;


        if(playerListTransform == null) playerListTransform = GameObject.Find(playerListTransformName).transform;

        foreach (Transform playerUIItem in playerListTransform)
        {
            Destroy(playerUIItem.gameObject);
        }

        playersInRoom = roomSlots;
        for (int i = 0; i < playersInRoom.Count; i++)
        {
            CreatePlayerUIItem((RoomPlayerUI)playersInRoom[i]);
        }
    }

    private void CreatePlayerUIItem(RoomPlayerUI playerInfo)
    {
        GameObject playerUI = Instantiate(UIPrefab, playerListTransform);
        playerUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = playerInfo.playerName;
    }
}
