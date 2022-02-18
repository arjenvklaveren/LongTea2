using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Mirror;

public class CameraToPlayer : NetworkBehaviour
{
    private CinemachineVirtualCamera cinemachineCamera;
    public override void OnStartLocalPlayer()
    {
        cinemachineCamera = GameObject.FindGameObjectWithTag("MasterCMCamera").GetComponent<CinemachineVirtualCamera>();
        cinemachineCamera.Follow = transform;
        cinemachineCamera.LookAt = transform;
    }
}
