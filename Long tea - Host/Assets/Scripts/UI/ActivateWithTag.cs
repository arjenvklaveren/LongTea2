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
            foundObject.GetComponent<FadeUI>().FadeIn(0.5f);
        }
        else
        {
            Debug.Log("Can't find object");
        }
    }
}
