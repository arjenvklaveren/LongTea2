using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Ammo : NetworkBehaviour
{
    public int damage;
    NetworkRoomPlayer ammoOwner;

    public void SetOwner()
    {
        
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
        if (playerObject.TryGetComponent(out EntityHealth entityHealth))
        {
            entityHealth.DealDamage(damage);
            NetworkServer.Destroy(gameObject);
        }
    }
}
