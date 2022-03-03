using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateWithTag : MonoBehaviour
{
    public void ActiveObjectWithTag(string tagname)
    {
        GameObject foundObject = GameObject.FindGameObjectWithTag(tagname);
        if (foundObject)
        {
            foundObject.SetActive(true);
        }
        else
        {
            Debug.Log("Can't find object");
        }
    }

    public void FadeInObjectWithTag(string tagname)
    {
        GameObject foundObject = GameObject.FindGameObjectWithTag(tagname);
        if (foundObject)
        {
            if(foundObject.TryGetComponent(out FadeUI fadeUI)) fadeUI.FadeIn(0.5f);
            if (foundObject.TryGetComponent(out EventAfterTime eventAfterTime)) eventAfterTime.StartTimedEvent();
        }
        else
        {
            Debug.Log("Can't find object");
        }
    }
}
