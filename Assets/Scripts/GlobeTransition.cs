using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobeTransition : MonoBehaviour
{
    public float transitSpeed = 100f;
    public GameObject curGlobe;
    public GameObject nextGlobe;

    public void ChangeGlobe()
    {
        StartCoroutine(TransitRight());
    }
    private IEnumerator TransitRight()
    {
        while(transform.localPosition.x <= 15f)
        {
            transform.Translate(Vector3.right * Time.deltaTime * transitSpeed);
            yield return new WaitForEndOfFrame();
        }
        transform.localPosition += Vector3.left * 30f;

        curGlobe.SetActive(false);
        nextGlobe.SetActive(true);
        (curGlobe, nextGlobe) = (nextGlobe, curGlobe);
        
        StartCoroutine(TransitLeft());
    }
    private IEnumerator TransitLeft()
    {
        while(transform.localPosition.x + Time.deltaTime * transitSpeed < 0)
        {
            transform.Translate(Vector3.right * Time.deltaTime * transitSpeed);
            yield return new WaitForEndOfFrame();
        }
        transform.localPosition = new Vector3(0, transform.localPosition.y, transform.localPosition.z);
    }
}
