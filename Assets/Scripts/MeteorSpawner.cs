using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{

    private float radius, deltaMeteor;
    public GameObject asteroid;
    // Start is called before the first frame update
    void Start()
    {
        deltaMeteor = 1.0f;
        radius = 2.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (deltaMeteor < 0)
        {
            GameObject meteor = Instantiate(asteroid);
            float theta = Mathf.Rad2Deg * (2 * Mathf.PI * Random.Range(0, 1f));
            float phi = Mathf.Rad2Deg * (Mathf.PI * Random.Range(0, 1f));
            float x = Mathf.Sin(phi) * Mathf.Cos(theta);
            float y = Mathf.Sin(phi) * Mathf.Sin(theta);
            float z = Mathf.Cos(phi);
            meteor.transform.position = transform.position + new Vector3(x, y, z) * 4 * radius;
            deltaMeteor = Random.Range(0, 0.3f);
        }
        else
            deltaMeteor -= Time.deltaTime;
    }
}
