using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float xAxis = 0;
    public float yAxis = 45;
    public float zAxis = 0;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(xAxis, yAxis, zAxis) * Time.deltaTime);
    }
}