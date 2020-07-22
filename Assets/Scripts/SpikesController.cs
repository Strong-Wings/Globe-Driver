using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesController : MonoBehaviour
{
    public float time;
    private float timer;
    public float Height;
    private float curHeight;
    private bool flagUp = true;
    private bool flagDown = false;
    public Transform Globe;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (flagDown)
            StartCoroutine(DownCoroutine());
        else
        {
            if (flagUp)
            {
                timer -= Time.deltaTime;
            }
            if (flagUp && timer < 0)
            {
                timer = time;
                StartCoroutine(UpCoroutine());
            }
        }

    }

    IEnumerator UpCoroutine()
    {
        flagUp = false;
        while (curHeight < Height)
        {
            curHeight += 0.01f;
            this.transform.position = Vector3.MoveTowards(this.transform.position, Globe.position, -0.01f);
            //this.transform.localPosition += new Vector3(0, 0.1f, 0);
            yield return new WaitForSeconds(0.01f);
        }
        flagDown = true;
    }

    IEnumerator DownCoroutine()
    {
        flagDown = false;
        while (curHeight > 0)
        {
            curHeight -= 0.01f;
            this.transform.position = Vector3.MoveTowards(this.transform.position, Globe.position, 0.01f);
            yield return new WaitForSeconds(0.01f);
        }
        flagUp = true;
    }
}
