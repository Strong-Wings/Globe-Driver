using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private float radius, deltaMeteor, deltaGem;
    public static int gemAmount; 
    public GameObject asteroid;
    public GameObject Gem;
    public GameObject[] objects;
    //public GameObject explosion;
    List<GameObject> meteors = new List<GameObject>();
    private Vector3 center;
    // Start is called before the first frame update
    void Start()
    {
        center = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        deltaMeteor = 1f; deltaGem = 3f; gemAmount = 1;
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
            meteors.Add(meteor);
            deltaMeteor = Random.Range(0, 0.3f);
        }
        else
            deltaMeteor -= Time.deltaTime;

        if (deltaGem < 0 && gemAmount < 11)
        {
            GameObject gem = Instantiate(Gem);
            gem.SetActive(true);
            float theta = Mathf.Rad2Deg * (2 * Mathf.PI * Random.Range(0, 1f));
            float phi = Mathf.Rad2Deg * (Mathf.PI * Random.Range(0, 1f));
            float x = Mathf.Sin(phi) * Mathf.Cos(theta);
            float y = Mathf.Sin(phi) * Mathf.Sin(theta);
            float z = Mathf.Cos(phi);
            gem.transform.position = transform.position + new Vector3(x, y, z) * radius * 1.05f;
            gem.transform.LookAt(center);
            gem.transform.Rotate(180f, 0, 0);
            deltaGem = Random.Range(2f, 7f);
            gemAmount += 1;
        }
        else
            deltaGem -= Time.deltaTime; 

        foreach (var m in meteors)
        {
            if (m && (m.transform.position - center).magnitude < 0.9 * radius)
                Destroy(m);
        }
    }
}