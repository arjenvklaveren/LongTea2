using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DestoyAfterTime : NetworkBehaviour
{
    [SerializeField] private float delay = 5f;
    // Start is called before the first frame update
    void Start()
    {
        if (isServer)
        {
            TryDestroy();
        }
    }

    [Server]
    public void TryDestroy()
    {
        StartCoroutine(DestroyDelayed());
    }

    IEnumerator DestroyDelayed()
    {
        yield return new WaitForSeconds(delay);
        NetworkServer.Destroy(gameObject);
    }
}
