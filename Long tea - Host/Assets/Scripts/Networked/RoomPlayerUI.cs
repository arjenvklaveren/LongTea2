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

    [Header("Player settings")]
    [SyncVar] public string playerName = "Unknown name";
    [SyncVar(hook = "UpdateScoreLabel")] public int score = 0;

    private TextMeshProUGUI scoreLabel;

    public void CoupleScoreLabel(TextMeshProUGUI scoreLabelReference)
    {
        scoreLabel = scoreLabelReference;
    }

    public void UpdateScoreLabel(int oldScore, int newScore)
    {
        if (scoreLabel) scoreLabel.text = $"{newScore}";
    }

    [Command]
    public void ChangePlayerScore(int newScore)
    {
        score = newScore;
    }

    [Command]
    public void AddPlayerScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }

    [Command]
    public void ChangePlayerName(string newPlayerName)
    {
        playerName = newPlayerName;
    }

    [ContextMenu("Change name to random")]
    public string SetRandomPlayerName()
    {
        string[] randomNames = { "Gold eyed Ziggy", "Party Pipo", "Captain Pete", "Pesky Bird", "Astley Rick", "Gordon Freecat" };
        string randomName = randomNames[Random.Range(0, randomNames.Length)];

        ChangePlayerName(randomName);
        return randomName;
    }
}
