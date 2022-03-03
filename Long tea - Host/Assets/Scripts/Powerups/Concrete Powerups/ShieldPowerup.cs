using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ShieldPowerup : Powerup
{
    [SerializeField] int protectedHitAmount;

    int hits;

    public void OnTakeDamage()
    {
        hits++;
        if(hits > protectedHitAmount)
        {
            NetworkServer.Destroy(this.gameObject);
        }
    }
}
