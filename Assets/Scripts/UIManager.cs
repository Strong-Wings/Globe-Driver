using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [HideInInspector]
    public bool sound = true;
    [HideInInspector]
    public static bool pause = false;
    public float menuBarSpeed = 500f;
    public GameObject cam;
    public GameObject ShopCanvas;
    public GameObject[] menuObjects;
    public GameObject[] gameObjects;
    public GameObject gemsAmountField;
    public GameObject Globe;

    private UnityEngine.UI.Text textField;
    private float t = 0, _tmp = 0, _endOfBar = 270f;
    private bool _barOpening = false;

    private void Start()
    {
        textField = gemsAmountField.GetComponent<UnityEngine.UI.Text>();
        Globe.GetComponent<CrystalSpawner>().enabled = false;
       // Globe.GetComponent<MeteorSpawner>().enabled = false;
        if (!System.IO.File.Exists(Application.persistentDataPath + "/balance.gd"))
        {
            StreamWriter balancedataW = new StreamWriter(Application.persistentDataPath + "/balance.gd");
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
        //Globe.GetComponent<MeteorSpawner>().enabled = true;
        PlayerController.StartGame = true;
    }
    public void SoundTurn()
    {
        sound = !sound;
        transform.Find("SoundOn").gameObject.SetActive(sound);
        transform.Find("SoundOff").gameObject.SetActive(!sound);
    }
    public void ShowShop(bool show)
    {
        ShopCanvas.SetActive(show);
    }
    public void GoToHome()
    {
        SceneManager.LoadScene("Main");
        UIManager.pause = false;
    }
    public void Restart()
    {
        GoToHome();
        StartPlay();
    }
    public void MenuButton(GameObject MenuBar)
    {
        if (_barOpening) return;
        _barOpening = true;
        MenuBar.transform.GetChild(3).gameObject.SetActive(pause);
        MenuBar.transform.GetChild(4).gameObject.SetActive(!pause);
        MenuBar.transform.GetChild(5).gameObject.SetActive(!pause);
        MenuBar.transform.GetChild(6).gameObject.SetActive(!pause);
        if (!pause)
        {
            StartCoroutine(MenuBarTransition(MenuBar.transform.GetChild(1).GetComponent<RectTransform>(), MenuBar.transform.GetChild(2).GetComponent<RectTransform>()));
        }
        else
        {
            StartCoroutine(MenuBarTransitionBack(MenuBar.transform.GetChild(1).GetComponent<RectTransform>(), MenuBar.transform.GetChild(2).GetComponent<RectTransform>()));
        }
    }
    private IEnumerator MenuBarTransition(RectTransform panel, RectTransform rightCircle)
    {
        while(panel.sizeDelta.x + Time.deltaTime * menuBarSpeed <= _endOfBar)
        {
            panel.sizeDelta += Vector2.right * Time.deltaTime * menuBarSpeed;
            rightCircle.anchoredPosition += Vector2.right * Time.deltaTime * menuBarSpeed;
            yield return null;
        }
        panel.sizeDelta = new Vector2(_endOfBar, panel.sizeDelta.y);
        rightCircle.anchoredPosition = new Vector2(43f + _endOfBar, 0f);
        pause = true;
        _barOpening = false;
    }
    private IEnumerator MenuBarTransitionBack(RectTransform panel, RectTransform rightCircle)
    {
        while (panel.sizeDelta.x - Time.deltaTime* menuBarSpeed >= 0)
        {
            panel.sizeDelta -= Vector2.right * Time.deltaTime * menuBarSpeed;
            rightCircle.anchoredPosition -= Vector2.right * Time.deltaTime * menuBarSpeed;
            yield return null;
        }
        panel.sizeDelta = new Vector2(0, panel.sizeDelta.y);
        rightCircle.anchoredPosition = new Vector2(43f, 0f);
        pause = false;
        _barOpening = false;
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