using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SpectatorCamera : MonoBehaviour
{
    private List<Transform> interestingObjects = new List<Transform>();
    [SerializeField] private List<CloseObjects> shipRelations = new List<CloseObjects>();
    private CinemachineTargetGroup cinemachineTargetGroup;
    [SerializeField] private CinemachineVirtualCamera cinemachineMainCamera;
    [SerializeField] private bool isSpectatingEveryone = true;

    private Transform currentlySpectating;
    private int currentIndex = 1;

    // Start is called before the first frame update
    void Start()
    {
        cinemachineTargetGroup = FindObjectOfType<CinemachineTargetGroup>();
        Invoke("FetchInterestingObjects", 4f);

        if (isSpectatingEveryone) InvokeRepeating("UpdateInteresting", 5f, 2f);
    }


    //TODO: Call this after any (other) player leaves
    private void FetchInterestingObjects()
    {
        GameObject[] interestingGameObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject interesting in interestingGameObjects)
        {
            interestingObjects.Add(interesting.transform);
        }
    }

    private void CheckInterestingObjectValidity()
    {
        List<Transform> objectsToRemove = new List<Transform>();
        for (int i = 0; i < interestingObjects.Count; i++)
        {
            if (interestingObjects[i] == null)
            {
                objectsToRemove.Add(interestingObjects[i]);
            }
        }

        if (objectsToRemove.Count > 0)
        {
            for (int j = 0; j < objectsToRemove.Count; j++)
            {
                interestingObjects.Remove(objectsToRemove[j]);
            }
        }
    }

    private void UpdateInteresting()
    {
        CheckInterestingObjectValidity();
        shipRelations.Clear();
        if (interestingObjects.Count < 2)
        {
            return;
        }

        foreach (Transform potential in interestingObjects)
        {
            if (potential == null)
            {
                FetchInterestingObjects();
                return;
            }
            shipRelations.Add(FindClosest(potential, new List<Transform>(interestingObjects)));
        }

        shipRelations.Sort((ship1, ship2) => ship1.distanceBetween.CompareTo(ship2.distanceBetween));

        cinemachineTargetGroup.m_Targets[0].target = shipRelations[0].object1;
        cinemachineTargetGroup.m_Targets[1].target = shipRelations[0].object2;
    }

    public void SpectateNext()
    {
        CheckInterestingObjectValidity();

        if (interestingObjects.Contains(currentlySpectating))
        {
            currentIndex = interestingObjects.IndexOf(currentlySpectating);
        }

        currentIndex = currentIndex < interestingObjects.Count - 1 ? currentIndex + 1 : 0;

        Transform nextInteresting = interestingObjects[currentIndex];

        cinemachineMainCamera.Follow = nextInteresting;
        cinemachineMainCamera.LookAt = nextInteresting;
    }

    public void SpectatePrevious()
    {
        CheckInterestingObjectValidity();

        if (interestingObjects.Contains(currentlySpectating))
        {
            currentIndex = interestingObjects.IndexOf(currentlySpectating);
        }

        currentIndex = currentIndex > 0 ? currentIndex - 1 : interestingObjects.Count - 1;

        Transform nextInteresting = interestingObjects[currentIndex];

        cinemachineMainCamera.Follow = nextInteresting;
        cinemachineMainCamera.LookAt = nextInteresting;
    }

    public void SetSpectatingEveryone(bool isSpectatingAll)
    {
        isSpectatingEveryone = isSpectatingAll;
        Invoke("FetchInterestingObjects", 4f);
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
