using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreventPhoneScreenSleep : MonoBehaviour
{
    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}
