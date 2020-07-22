using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Rendering;
using UnityEngine;

public class CrystalController : MonoBehaviour
{
    public float SpeedRotate;
    public ParticleSystem blast;
    
    void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * SpeedRotate);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) {
            Destroy(Instantiate(blast.gameObject, transform.position, Quaternion.identity), 2f);
            GameObject.Find("DiamondCollect").GetComponent<AudioSource>().Play();
            Destroy(this.gameObject);
            CrystalSpawner.gemAmount -= 1;
            PlayerController.gemAmount += 1;
        }
    }
}
