using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    private List<RoomPlayerUI> roomPlayers = new List<RoomPlayerUI>();
    [SerializeField] private GameObject scorePrefab;
    [SerializeField] private float spawnDelay = 1f;
    [SerializeField] private float initialSpawnDelay = 1f;
    [SerializeField] private Transform spawnParent;
    [SerializeField] private Sprite firstPlaceIcon;
    [SerializeField] private Sprite secondPlaceIcon;
    [SerializeField] private Sprite thirdPlaceIcon;

    public void Start()
    {
        RoomPlayerUI[] roomPlayersInScene = GameObject.FindObjectsOfType<RoomPlayerUI>();
        roomPlayers.AddRange(roomPlayersInScene);
        SortOnScore();
        StartCoroutine(SpawnPlayersDelayed());
    }

    public void SortOnScore()
    {
        roomPlayers = roomPlayers.OrderByDescending(player => player.score).ToList();
    }

    IEnumerator SpawnPlayersDelayed()
    {
        yield return new WaitForSeconds(initialSpawnDelay);
        for (int i = 0; i < roomPlayers.Count; i++)
        {
            GameObject scoreItem = Instantiate(scorePrefab, spawnParent);
            Image scoreboardIcon = scoreItem.transform.GetChild(1).GetComponent<Image>();

            if (i == 0 && firstPlaceIcon) scoreboardIcon.sprite = firstPlaceIcon;
            if (i == 1 && secondPlaceIcon) scoreboardIcon.sprite = secondPlaceIcon;
            if (i == 2 && thirdPlaceIcon) scoreboardIcon.sprite = thirdPlaceIcon;

            scoreItem.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = $"{roomPlayers[i].playerName}\n{roomPlayers[i].score}";

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
