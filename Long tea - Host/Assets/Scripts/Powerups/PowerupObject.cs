using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Mirror;

public class PowerupObject : NetworkBehaviour
{
    [SerializeField] int minCoolDownTime;
    [SerializeField] int maxCoolDownTime;

    [SerializeField] private List<Powerup> availablePowerups = new List<Powerup>();

    void Start()
    {
        //StartCoroutine(CoolDown());
    }

    private void FixedUpdate()
    {
        transform.Rotate(0, 1, 0);
        transform.position = new Vector3(transform.position.x, transform.position.y + Mathf.Sin(Time.time * 3f) * 0.1f, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Player") && other.GetComponentInChildren<Powerup>() == null)
        {
            Powerup powerup = Instantiate(availablePowerups[Random.Range(0, availablePowerups.Count - 1)], other.transform);
            powerup.owner = other.gameObject;
            NetworkServer.Spawn(powerup.gameObject);
            StartCoroutine(CoolDown());
        }
    }

    IEnumerator CoolDown()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds(Random.Range(minCoolDownTime, maxCoolDownTime));
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
