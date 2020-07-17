using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    public float speed = 5f;
    
    void Start()
    {
    }

    void Update()
    {
        float y = Input.GetAxis("Horizontal");
        float x = Input.GetAxis("Vertical");

        transform.RotateAround(Vector3.up, y*Time.deltaTime);
        transform.RotateAround(Vector3.left, x*Time.deltaTime);

    }
    
}
