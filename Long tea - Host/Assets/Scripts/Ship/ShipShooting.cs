using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShipShooting : MonoBehaviour
{

    [SerializeField] private GameObject cannonHolderParent;
    [SerializeField] private List<GameObject> cannons = new List<GameObject>();
    [SerializeField] private GameObject cannonBall;


    [SerializeField] private float shotCooldown;
    [Range(0.05f, 1), SerializeField] private float barrelRotateSpeed;
    [Range(0, 90), SerializeField] private float barrelRotateDegrees;

    private float timeCounter;
    private bool isAiming;

    void Start()
    {
        foreach (Transform cannon in cannonHolderParent.transform)
        {
            cannons.Add(cannon.gameObject);
            cannon.transform.GetChild(0).eulerAngles = new Vector3(0, +cannon.transform.eulerAngles.y, -(barrelRotateDegrees / 2));
        }
    }
    private void Update()
    {
        DetectTouch();
    }

    void DetectTouch()
    {
        if (Input.touchCount > 0)
        {
            isAiming = true;
            foreach (GameObject cannon in cannons)
            {
                RotateBarrels(cannon);
            }
        }
        else if(Input.touchCount == 0 && isAiming)
        {
            foreach (GameObject cannon in cannons)
            {
                StartCoroutine(ShootCannons(cannon));
            }
            isAiming = false;
        }
    }

    void RotateBarrels(GameObject cannon)
    {
        timeCounter += Time.deltaTime * barrelRotateSpeed;
        cannon.transform.GetChild(0).eulerAngles = new Vector3(0, +cannon.transform.eulerAngles.y, -(Mathf.Sin(timeCounter) + 1) / 2)  * barrelRotateDegrees;
    }

    IEnumerator ShootCannons(GameObject cannon)
    {
        Debug.Log("SHOOT");
        GameObject cannonBallCopy = Instantiate(cannonBall);
        cannonBallCopy.transform.position = cannon.transform.GetChild(0).GetChild(0).GetChild(0).transform.position;
        yield return new WaitForSeconds(0);
    }
}
