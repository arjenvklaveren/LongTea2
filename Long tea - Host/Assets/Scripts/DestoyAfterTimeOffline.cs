using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DestoyAfterTimeOffline : MonoBehaviour
{
    [SerializeField] private float destroyDelay = 5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyDelayed(destroyDelay));
    }


    IEnumerator DestroyDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
