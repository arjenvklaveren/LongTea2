using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ShootCannonballOffline : MonoBehaviour
{
    [SerializeField] private GameObject cannonBallPrefab;

    public void ShootBall(Vector3 spawnPosition, Quaternion spawnOrientation)
    {
        GameObject cannonBallCopy = Instantiate(cannonBallPrefab, spawnPosition, spawnOrientation);
    }
}
