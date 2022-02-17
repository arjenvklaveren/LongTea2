using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ShootCannonball : NetworkBehaviour
{
    [SerializeField] private GameObject cannonBallPrefab;

    [Command(requiresAuthority = false)]
    public void ShootBall(Vector3 spawnPosition, Quaternion spawnOrientation)
    {
        GameObject cannonBallCopy = Instantiate(cannonBallPrefab, spawnPosition, spawnOrientation);
        NetworkServer.Spawn(cannonBallCopy);
    }
}
