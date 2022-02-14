using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class RoomPlayerUI : NetworkRoomPlayer
{
    [Header("Room listing")]
    [SerializeField] private GameObject UIPrefab;
    [SerializeField] private string playerListTransformName = "Playerlist";

    private Transform playerListTransform;
    private List<NetworkRoomPlayer> playersInRoom;
    

    
}
