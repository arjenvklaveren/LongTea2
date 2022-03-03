using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SpeedPowerup : Powerup
{
    int speedUpTime;
    private void Start()
    {
        StartCoroutine(SpeedUp());
    }


    IEnumerator SpeedUp()
    {
        owner.GetComponent<ShipGyroControlsNetworked>().moveSpeedRate = 10000;
        yield return new WaitForSeconds(speedUpTime);
        owner.GetComponent<ShipGyroControlsNetworked>().moveSpeedRate = 5000;
        NetworkServer.Destroy(this.gameObject);
    }
}
