using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Ammo : NetworkBehaviour
{
    public int damage;
    public int scoreAmount = 20;
    RoomPlayerUI owner;

    void Start()
    {
        Debug.Log("CANNONBALL");
        Debug.Log($"Local player is {isLocalPlayer}");
        Debug.Log($"Is server is {isServer}");
        Debug.Log($"Has authority is {hasAuthority}");

        RoomPlayerUI[] playerUI = GameObject.FindObjectsOfType<RoomPlayerUI>();
        if (hasAuthority)
        {
            Debug.Log("Doing things with players and such");
            foreach (RoomPlayerUI playerUIInstance in playerUI)
            {
                Debug.Log(playerUIInstance.playerName);
                if (playerUIInstance.hasAuthority)
                {
                    owner = playerUIInstance;
                    Debug.Log("Owner is " + playerUIInstance.playerName);
                    break;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            TryDealDamage(collision.gameObject);
        }
    }


    public void TryDealDamage(GameObject playerObject)
    {
        if (playerObject.TryGetComponent(out EntityHealth entityHealth) && entityHealth.canTakeDamage && !entityHealth.isDead)
        {
            entityHealth.DealDamage(damage);
            if (owner) owner.AddPlayerScore(scoreAmount);
            NetworkServer.Destroy(gameObject);
        }
    }
}
