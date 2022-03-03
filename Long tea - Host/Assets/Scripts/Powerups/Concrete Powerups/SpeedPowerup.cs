using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerup : Powerup
{

    private void Start()
    {
        
    }


    IEnumerator SpeedUp()
    {
        yield return new WaitForSeconds(10);
    }
}
