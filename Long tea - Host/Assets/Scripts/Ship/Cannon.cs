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
    public ShootCannonballOffline shootCannonballReferenceOffline;

    public void Shoot()
    {
        shootSound.Play();
        if (shootCannonballReference != null)
        {
            shootCannonballReference.ShootBall(barrelTip.position, barrelPivot.rotation);
        }
        else
        {
            shootCannonballReferenceOffline.ShootBall(barrelTip.position, barrelPivot.rotation);
        }
    }
}
