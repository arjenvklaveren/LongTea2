using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class RoomPlayerUI : NetworkRoomPlayer
{
    [SerializeField] private GameObject UIPrefab;
    [SerializeField] private string playerListTransformName = "Playerlist";

    private Transform playerListTransform;

    public override void OnStartClient()
    {
        if (isLocalPlayer)
        {
            base.OnStartClient();
            playerListTransform = GameObject.Find(playerListTransformName).transform;
        }
    }

    public override void OnClientEnterRoom()
    {
        base.OnClientEnterRoom();

        if (isLocalPlayer)
            StartCoroutine(TryDrawUI());
    }

    public override void OnClientExitRoom()
    {
        base.OnClientExitRoom();

        if (isLocalPlayer)
            StartCoroutine(TryDrawUI());
    }

    public IEnumerator TryDrawUI()
    {
        yield return new WaitForSeconds(0.1f);
        NetworkRoomManager room = NetworkManager.singleton as NetworkRoomManager;
        if (room)
        {
            if (!NetworkManager.IsSceneActive(room.RoomScene))
                yield break;

            foreach(Transform playerUIItem in playerListTransform)
            {
                Destroy(playerUIItem.gameObject);
            }

            List<NetworkRoomPlayer> playersInRoom = room.roomSlots;
            for (int i = 0; i < playersInRoom.Count; i++)
            {
                CreatePlayerUIItem(playersInRoom[i]);
            }
        }
    }

    private void CreatePlayerUIItem(NetworkRoomPlayer playerInfo)
    {
        GameObject playerUI = Instantiate(UIPrefab, playerListTransform);
        playerUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"Player {playerInfo.index}";
    }
}
