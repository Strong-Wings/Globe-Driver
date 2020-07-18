using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private float radius; 
    public GameObject[] objects;
 
    private Vector3 center;
    // Start is called before the first frame update
    void Start()
    {
        center = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        radius = 2.5f;
        for (int i = 0; i < 20; i++)
        {

            //GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            GameObject cylinder = Instantiate(objects[Random.Range(0, 2)]);
            float theta = Mathf.Rad2Deg * (2 * Mathf.PI * Random.Range(0, 1f));
            float phi = Mathf.Rad2Deg * (Mathf.PI * Random.Range(0, 1f));
            float x = Mathf.Sin(phi) * Mathf.Cos(theta);
            float y = Mathf.Sin(phi) * Mathf.Sin(theta);
            float z = Mathf.Cos(phi);
            cylinder.transform.position = transform.position + new Vector3(x, y, z) * radius;
            //cylinder.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            cylinder.transform.LookAt(center);
            cylinder.transform.Rotate(-90f, 0, 0);
            cylinder.transform.parent = transform;
        }
    }

}