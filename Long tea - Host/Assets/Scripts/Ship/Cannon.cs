using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Mirror;

public class Cannon : MonoBehaviour
{
    public Transform barrelTip;
    public Transform barrelPivot;
    public AudioSource shootSound;
    public ShootCannonball shootCannonballReference;

    public void Shoot()
    {
        shootSound.Play();
        shootCannonballReference.ShootBall(barrelTip.position, barrelPivot.rotation);
    }
}
