using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [HideInInspector]
    public bool sound = true;
    public GameObject cam;
    public GameObject[] menuObjects;
    public GameObject[] gameObjects;

    private float t = 0;

    public void StartPlay()
    {
        StartCoroutine(CamToGlobe());
        for (int i = 0; i < menuObjects.Length; i++)
            menuObjects[i].SetActive(false);
        for (int i = 0; i < gameObjects.Length; i++)
            gameObjects[i].SetActive(true);
    }
    public void SoundTurn()
    {
        sound = !sound;
    }
    public void ShowShop()
    {
        Debug.Log("Shop");
    }

    private IEnumerator CamToGlobe()
    {
        while(cam.transform.localPosition.z < -3 || cam.transform.localPosition.y > 6)
        {
            cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, new Vector3(0, 6, -3), t += Time.deltaTime);
            yield return null;
        }
    }
}
// 11 -9 -> 0 6 -3