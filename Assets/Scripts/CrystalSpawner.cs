using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalSpawner : MonoBehaviour
{

    private float radius, deltaGem;
    public static int gemAmount;
    public GameObject Gem;
    private Vector3 center;
    // Start is called before the first frame update
    void Start()
    {
        center = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        deltaGem = 3f; gemAmount = 1;
        radius = 2.5f;
    }

    // Update is called once per frame
    void Update()
    {
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
    }
}
