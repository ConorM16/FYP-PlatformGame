using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPCameraControl : MonoBehaviour
{
    protected float fDistance = 1;
    protected float fSpeed = 1;
    public GameObject Player;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("1")) OrbitTower(false);
        if (Input.GetKey("2")) OrbitTower(true);
    }

    protected void OrbitTower(bool bLeft)
    {
        float step = fSpeed * Time.deltaTime;
        float fOrbitCircumfrance = 2F * fDistance * Mathf.PI;
        float fDistanceDegrees = (fSpeed / fOrbitCircumfrance) * 360;
        float fDistanceRadians = (fSpeed / fOrbitCircumfrance) * 2 * Mathf.PI;
        if (bLeft)
        {
            transform.RotateAround(Player.transform.position, Vector3.up, -fDistanceRadians);
        }
        else
            transform.RotateAround(Player.transform.position, Vector3.up, fDistanceRadians);
    }
}
