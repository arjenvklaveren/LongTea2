using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class ShipShooting : MonoBehaviour
{
    [SerializeField] private int shootPower;
    [SerializeField] private float shootCooldown;
    [SerializeField] private float barrelMaxRotation;

    [SerializeField] private GameObject cannonHolderParent;
    [SerializeField] private GameObject cannonBall;

    [SerializeField] private CinemachineVirtualCamera shootPOVCamera;

    [SerializeField] private LineRenderer predictionRenderer1;
    [SerializeField] private LineRenderer predictionRenderer2;
    [Range(10, 100), SerializeField] private int predictionPointAmount;
    [Range(0.01f,1), SerializeField] private float predictionSmoothness;

    private bool canShoot = true;

    private bool isAiming;
    private List<Cannon> cannons = new List<Cannon>();
    [SerializeField]  private List<Cannon> selectedCannons = new List<Cannon>();

    void Start()
    {
        foreach (Transform cannon in cannonHolderParent.transform)
        {
            cannons.Add(cannon.GetComponent<Cannon>());
        }
    }
    private void Update()
    {
        DetectTouch();
    }

    void DetectTouch()
    {
        Debug.Log(Input.touchCount);
        //Aiming
        if (Input.touchCount == 1)
        {
            var touch = Input.touches[0];
            shootPOVCamera.Priority = 15;

            //Touched on right side
            if (touch.position.x > Screen.width/2)
            {
                SetupPOVShooting(90, true);
            }
            //Touched on left side
            else if(touch.position.x < Screen.width / 2)
            {
                SetupPOVShooting(-90, false);
            }

            IsAiming();        
        }

        //Not aiming anymore, so shooting if able
        else if(Input.touchCount != 1 && isAiming)
        {
            if (canShoot)
            {
                foreach (Cannon cannon in selectedCannons)
                {
                    StartCoroutine(ShootCannons(cannon));
                }          
                canShoot = false;
                StartCoroutine(ShootingCooldown());
            }
            shootPOVCamera.Priority = 5;                 
            predictionRenderer1.positionCount = 0;
            predictionRenderer2.positionCount = 0;
            selectedCannons.Clear();
            isAiming = false;
        }
    }

    void SetupPOVShooting(int cameraAngle, bool isRight)
    {
        shootPOVCamera.transform.eulerAngles = new Vector3(0, cameraAngle + transform.localEulerAngles.y, 0);
        if (!isAiming)
        {
            foreach (Cannon cannon in cannons)
            {
                if (isRight)
                {
                    if (cannon.isPositionedOnRightSideOfShip)
                    {
                        selectedCannons.Add(cannon);
                    }
                }
                if(!isRight)
                {
                    if (!cannon.isPositionedOnRightSideOfShip)
                    {
                        selectedCannons.Add(cannon);
                    }
                }
            }
        }
    }

    void IsAiming()
    {
        isAiming = true;
        DrawShootingPrediction();
        foreach (Cannon cannon in selectedCannons)
        {
            RotateBarrels(cannon);
        }
    }

    void RotateBarrels(Cannon cannon)
    {
        if (Input.acceleration.z + 1 < 1 && Input.acceleration.z + 1 > 0)
        {
            cannon.barrelPivot.transform.localEulerAngles = new Vector3(-((Input.acceleration.z + 1) * barrelMaxRotation), 0, 0);
        }
    }

    void DrawShootingPrediction()
    {
        predictionRenderer1.positionCount = (int)predictionPointAmount;
        predictionRenderer2.positionCount = (int)predictionPointAmount;

        List<Vector3> points = new List<Vector3>();
        Vector3 startingPosition = selectedCannons[1].barrelTip.position;
        Vector3 startingVelocity = selectedCannons[1].barrelTip.transform.forward * shootPower;
        for (float t = 0; t < predictionPointAmount; t += predictionSmoothness / 2)
        {
            Vector3 newPoint = startingPosition + t * startingVelocity;
            newPoint.y = startingPosition.y + startingVelocity.y * t + Physics.gravity.y / 2f * t * t;
            points.Add(newPoint);

            if(newPoint.y <= 0)
            {
                predictionRenderer1.positionCount = points.Count;
                predictionRenderer2.positionCount = points.Count;
                break;
            }
        }

        predictionRenderer1.SetPositions(points.ToArray());
        predictionRenderer2.SetPositions(points.ToArray());
    }

    IEnumerator ShootingCooldown()
    {
        predictionRenderer1.startColor = new Color(255, 0, 0);
        predictionRenderer1.endColor = new Color(255, 0, 0);
        predictionRenderer2.startColor = new Color(255, 0, 0);
        predictionRenderer2.endColor = new Color(255, 0, 0);
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
        predictionRenderer1.startColor = new Color(255, 255, 255);
        predictionRenderer1.endColor = new Color(255, 255, 255);
        predictionRenderer2.startColor = new Color(255, 255, 255);
        predictionRenderer2.endColor = new Color(255, 255, 255);
    }

    IEnumerator ShootCannons(Cannon cannon)
    {
        yield return new WaitForSeconds(Random.Range(0f, 0.25f));
        cannon.Shoot(cannonBall, shootPower);       
    }
}
