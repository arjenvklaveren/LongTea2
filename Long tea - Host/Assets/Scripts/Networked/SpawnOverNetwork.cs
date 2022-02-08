using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SpawnOverNetwork : NetworkBehaviour
{
    [SerializeField] private GameObject objectToSpawn = null;
    [SerializeField] private Transform spawnTransform;

    [Command(requiresAuthority = false)]
    public void SpawnItem()
    {
        GameObject spawnedObject = Instantiate(objectToSpawn, spawnTransform.position, spawnTransform.rotation);
        NetworkServer.Spawn(spawnedObject);
    }
}
