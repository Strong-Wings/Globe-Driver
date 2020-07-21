using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ChecksMover : MonoBehaviour
{

    public Transform Puzzle;

    void Start()
    {
        Puzzle.localScale *= 0;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(MoveCoroutine());
            this.transform.localScale *= 0;
        }
    }

    IEnumerator MoveCoroutine()
    {
        while (Puzzle.localScale != new Vector3(1, 1, 1))
        {
            Puzzle.localScale += new Vector3(0.01f, 0.01f, 0.01f);
            yield return new WaitForSeconds(0.01f);
        }
        this.gameObject.SetActive(false);
    }
}
