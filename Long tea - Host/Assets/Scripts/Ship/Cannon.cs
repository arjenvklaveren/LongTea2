using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Transform barrelTip;
    public Transform barrelPivot;
    public bool isPositionedOnRightSideOfShip;
    public AudioSource shootSound;

    public void Shoot(GameObject cannonBall, float shotPower)
    {
        GameObject cannonBallCopy = Instantiate(cannonBall, barrelTip.position, barrelPivot.rotation);
        cannonBallCopy.GetComponent<Rigidbody>().AddForce(barrelPivot.transform.TransformDirection(new Vector3(0,0, shotPower)), ForceMode.VelocityChange);
        shootSound.Play();
    }
}
