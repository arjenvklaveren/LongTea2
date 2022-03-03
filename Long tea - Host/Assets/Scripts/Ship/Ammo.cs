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
        RoomPlayerUI[] playerUI = GameObject.FindObjectsOfType<RoomPlayerUI>();
        if (hasAuthority)
        {
            foreach (RoomPlayerUI playerUIInstance in playerUI)
            {
                if (playerUIInstance.hasAuthority)
                {
                    owner = playerUIInstance;
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
