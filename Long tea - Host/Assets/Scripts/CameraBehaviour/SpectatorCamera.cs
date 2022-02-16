using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SpectatorCamera : MonoBehaviour
{
    private List<Transform> interestingObjects = new List<Transform>();
    [SerializeField] private List<CloseObjects> shipRelations = new List<CloseObjects>();
    private CinemachineTargetGroup cinemachineTargetGroup;

    // Start is called before the first frame update
    void Start()
    {
        cinemachineTargetGroup = FindObjectOfType<CinemachineTargetGroup>();
        Invoke("FetchInterestingObjects", 4f);
        InvokeRepeating("UpdateInteresting", 5f, 2f);
    }

    public void FetchInterestingObjects()
    {
        GameObject[] interestingGameObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject interesting in interestingGameObjects)
        {
            interestingObjects.Add(interesting.transform);
        }
    }

    public void UpdateInteresting()
    {
        shipRelations.Clear();
        if (interestingObjects.Count < 2)
        {
            return;
        }

        foreach (Transform potential in interestingObjects)
        {
            shipRelations.Add(FindClosest(potential, new List<Transform>(interestingObjects)));
        }

        shipRelations.Sort((ship1, ship2) => ship1.distanceBetween.CompareTo(ship2.distanceBetween));

        cinemachineTargetGroup.m_Targets[0].target = shipRelations[0].object1;
        cinemachineTargetGroup.m_Targets[1].target = shipRelations[0].object2;
    }

    private CloseObjects FindClosest(Transform currentObject, List<Transform> otherObjects)
    {
        Transform bestTarget = null;
        otherObjects.Remove(currentObject);
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = currentObject.position;
        for (int i = 0; i < otherObjects.Count; i++)
        {
            Vector3 directionToTarget = otherObjects[i].position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = otherObjects[i];
            }
        }

        CloseObjects currentClosestObject = new()
        {
            object1 = currentObject,
            object2 = bestTarget,
            distanceBetween = closestDistanceSqr
        };

        return currentClosestObject;
    }

    [System.Serializable]
    public struct CloseObjects
    {
        public Transform object1;
        public Transform object2;
        public float distanceBetween;
    }
}
