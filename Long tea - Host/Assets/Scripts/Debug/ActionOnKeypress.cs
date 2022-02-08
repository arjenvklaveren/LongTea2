using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Events;

public class ActionOnKeypress : NetworkBehaviour
{
    [SerializeField] private KeyCode keyToPress = KeyCode.Space;
    [SerializeField] private UnityEvent eventToExecute;


    // Update is called once per frame
    void Update()
    {
        if(isLocalPlayer && Input.GetKeyDown(keyToPress))
        {
            eventToExecute.Invoke();
        }
    }
}
