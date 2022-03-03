using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Events;

public class MinimumPlayerCount : MonoBehaviour
{
    [SerializeField] private int minimalConnectedPlayers = 2;
    [SerializeField] private UnityEvent OnEnoughPlayers;
    [SerializeField] private UnityEvent OnNotEnoughPlayers;

    private bool hasEnough = false;

    private void Start()
    {
        OnNotEnoughPlayers.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        if (NetworkManager.singleton != null)
        {
            Debug.Log($"Checking playerCount: {NetworkManager.singleton.numPlayers}");
            if (NetworkManager.singleton.numPlayers >= minimalConnectedPlayers)
            {
                if (!hasEnough)
                {
                    hasEnough = true;
                    OnEnoughPlayers.Invoke();
                    Debug.Log("Has enough players");
                }
            }
            else
            {
                if (hasEnough)
                {
                    hasEnough = false;
                    OnNotEnoughPlayers.Invoke();
                    Debug.Log("Does not have enough players");
                }
            }
        }
    }
}
