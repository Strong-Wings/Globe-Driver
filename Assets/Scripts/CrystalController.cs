using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Rendering;
using UnityEngine;

public class CrystalController : MonoBehaviour
{
    // Start is called before the first frame update
    public float SpeedRotate;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime * SpeedRotate);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) {
            Destroy(this.gameObject);
            CrystalSpawner.gemAmount -= 1;
            PlayerController.gemAmount += 1;
        }
    }
}
