using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using System.IO;

public class UIManager : MonoBehaviour
{
    [HideInInspector]
    public bool sound = true;
    public GameObject cam;
    public GameObject ShopCanvas;
    public GameObject[] menuObjects;
    public GameObject[] gameObjects;
    public GameObject gemsAmountField;
    public GameObject Globe;
    private UnityEngine.UI.Text textField;
    private float t = 0;

    private void Start()
    {
        textField = gemsAmountField.GetComponent<UnityEngine.UI.Text>();
        Globe.GetComponent<CrystalSpawner>().enabled = false;
        Globe.GetComponent<MeteorSpawner>().enabled = false;
        if (!System.IO.File.Exists(Application.persistentDataPath + "/balance.gd"))
        {
            StreamWriter balancedataW = new StreamWriter(Application.persistentDataPath + "/balance.gd");
            PlayerController.gemAmount = 0;
            balancedataW.Write(0);
            balancedataW.Close();
        }
        else
        {
            StreamReader balancedata = new StreamReader(Application.persistentDataPath + "/balance.gd");
            PlayerController.gemAmount = int.Parse(balancedata.ReadLine());
            balancedata.Close();
        }
        textField.text = PlayerController.gemAmount.ToString();
    }
    public void StartPlay()
    {
        StartCoroutine(CamToGlobe());
        for (int i = 0; i < menuObjects.Length; i++)
            menuObjects[i].SetActive(false);
        for (int i = 0; i < gameObjects.Length; i++)
            gameObjects[i].SetActive(true);
        Globe.GetComponent<CrystalSpawner>().enabled = true;
        Globe.GetComponent<MeteorSpawner>().enabled = true;
        PlayerController.StartGame = true;
    }
    public void SoundTurn()
    {
        sound = !sound;
    }
    public void ShowShop(bool show)
    {
        ShopCanvas.SetActive(show);
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