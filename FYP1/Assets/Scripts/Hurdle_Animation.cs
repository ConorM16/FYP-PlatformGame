using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurdle_Animation : MonoBehaviour
{
    //adjust this to change speed
    float speed = 2f;
    //adjust this to change how high it goes
    float height = 0.5f;

    float firstY;

    void Start()
    {
        firstY = transform.position.y;
    }

    void Update()
    {
        //get the objects current position and put it in a variable so we can access it later with less code
        Vector3 pos = transform.position;
        //Debug.Log(pos);
        //calculate what the new Y position will be
        float newY = Mathf.Sin(Time.time * speed);
        newY = newY * height + firstY;
        //set the object's Y to the new calculated Y
        transform.position = new Vector3(pos.x, newY, pos.z);
    }
}
