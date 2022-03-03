using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class CoupleUIToPlayer : NetworkBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI scoreTextReference;
    [SerializeField] private Image healthBar;

    public void Start()
    {
        Invoke("CoupleScore", 4f);
        Invoke("CoupleHealth", 4f);
    }

    public void CoupleScore()
    {
        RoomPlayerUI[] playerUI = FindObjectsOfType<RoomPlayerUI>();
        foreach (RoomPlayerUI playerUIInstance in playerUI)
        {
            if (playerUIInstance.hasAuthority)
            {
                playerUIInstance.CoupleScoreLabel(scoreTextReference);
                break;
            }
        }
    }

    public void CoupleHealth()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject playerInstance in players)
        {
            if (playerInstance.GetComponent<NetworkIdentity>().hasAuthority)
            {
                if (playerInstance.TryGetComponent(out EntityHealth playerEntityHealth))
                {
                    playerEntityHealth.CoupleHealthbar(healthBar);
                    break;
                }
            }
        }
    }
}
