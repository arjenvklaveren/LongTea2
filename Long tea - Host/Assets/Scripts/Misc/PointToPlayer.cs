using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointToPlayer : MonoBehaviour
{
    [SerializeField] private Transform transformToRotate;
    [SerializeField] private GameObject ownShip;
    [SerializeField] private List<Transform> playerTransforms = new List<Transform>();
    [SerializeField] private float rotationSmoothing = 10f;
    private Quaternion targetRotation;

    private void Start()
    {
        Invoke("GetPlayers", 2f);
        InvokeRepeating("UpdateTargetRotation", 3f, 2f);
    }

    private void GetPlayers()
    {
        GameObject[] playersInScene = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < playersInScene.Length; i++)
        {
            if(playersInScene[i] != ownShip)
            playerTransforms.Add(playersInScene[i].transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transformToRotate.rotation = Quaternion.Lerp(transformToRotate.rotation, targetRotation, rotationSmoothing * Time.deltaTime);
    }

    private void UpdateTargetRotation()
    {
        targetRotation = Quaternion.LookRotation(GetClosestPlayer().position - transformToRotate.position, transform.forward);
        targetRotation = Quaternion.Euler(targetRotation.eulerAngles.x, targetRotation.eulerAngles.y, -90);
    }

    private Transform GetClosestPlayer()
    {
        Transform bestTarget = null;

        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transformToRotate.position;
        for (int i = 0; i < playerTransforms.Count; i++)
        {
            Vector3 directionToTarget = playerTransforms[i].position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = playerTransforms[i];
            }
        }

        return bestTarget;
    }
}
