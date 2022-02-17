using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SpawnOverNetwork : NetworkBehaviour
{
    [SerializeField] private GameObject objectToSpawn = null;
    [SerializeField] private Transform spawnTransform;
    public static SpawnOverNetwork Instance = null;

    private void Awake()
    {
        Instance = this;
    }

    [Command(requiresAuthority = false)]
    public void SpawnItem()
    {
        GameObject spawnedObject = Instantiate(objectToSpawn, spawnTransform.position, spawnTransform.rotation);
        NetworkServer.Spawn(spawnedObject);
    }

    [Command(requiresAuthority = false)]
    public void SpawnExisting(GameObject objectToSpawn)
    {
        Debug.Log($"Trying to spawn {objectToSpawn.name}");
        NetworkServer.Spawn(objectToSpawn);
    }
}
