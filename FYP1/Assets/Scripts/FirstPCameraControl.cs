using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPCameraControl : MonoBehaviour
{
    protected float fDistance = 1;
    protected float fSpeed = 2;
    public GameObject player;
    private Transform target;
    private Vector3 offset;
    float fOrbitCircumfrance;

    void Start()
    {
        offset = transform.position - player.transform.position;
        target = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (Input.GetKey("1"))
            {
                OrbitTower(false);
            }
            if (Input.GetKey("2"))
            {
                OrbitTower(true);
            }
            if (Input.GetKey("3"))
            {
                transform.LookAt(target);
            }
            transform.position = player.transform.position + offset;
        }
    }

   /* void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }*/

    protected void OrbitTower(bool bLeft)
    {
        if (player != null)
        {
            float step = fSpeed * Time.deltaTime;
            float fOrbitCircumfrance = 2F * fDistance * Mathf.PI;
            float fDistanceDegrees = (fSpeed / fOrbitCircumfrance) * 360;
            float fDistanceRadians = (fSpeed / fOrbitCircumfrance) * 2 * Mathf.PI;
            if (bLeft)
            {
                transform.RotateAround(player.transform.position, Vector3.up, -fDistanceRadians);
            }
            else
                transform.RotateAround(player.transform.position, Vector3.up, fDistanceRadians);
        }
    }
}
