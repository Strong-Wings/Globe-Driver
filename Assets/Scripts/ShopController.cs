using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
public class Car
{
    public GameObject model;
    public int cost;
    public bool bought;
}

public class ShopController : MonoBehaviour
{
    public GameObject carsPanel;
    public GameObject equipButton;
    public GameObject costButton;
    public float offset = 190;
    public float carRotationSpeed = 80f;
    public Car[] carInfo;

    private GameObject[] cars;
    private int _curCar;
    private float _turnSpeed = 1100f;
    private bool _turning = false;

    private void Start()
    {
        cars = new GameObject[carInfo.Length];
        for (int i = 0; i < cars.Length; i++)
        {
            cars[i] = Instantiate(carInfo[i].model, carsPanel.transform);
            cars[i].transform.localScale /= 2f;
            cars[i].transform.localPosition = new Vector3(offset * i, 0, -1000);
        }
        if (PlayerPrefs.HasKey("CurrentCar"))
            _curCar = PlayerPrefs.GetInt("CurrentCar");
        else
            _curCar = 0;
        carsPanel.transform.localPosition = new Vector3(-cars[_curCar].transform.localPosition.x, 0, 0);
        ChangeButton();
    }
    private void Update()
    {
        cars[_curCar].transform.Rotate(Vector3.up*carRotationSpeed*Time.deltaTime);
    }
    public void NextCar()
    {
        if (_turning || _curCar == cars.Length-1) return;
        _turning = true;
        cars[_curCar].transform.localRotation = Quaternion.Euler(Vector3.zero);
        _curCar++;
        StartCoroutine(TransitionNext(cars[_curCar].transform.localPosition));
    }
    public void PrevCar()
    {
        if (_turning || _curCar == 0) return;
        _turning = true;
        cars[_curCar].transform.localRotation = Quaternion.Euler(Vector3.zero);
        _curCar--;
        StartCoroutine(TransitionPrev(cars[_curCar].transform.localPosition));
    }
    private IEnumerator TransitionNext(Vector3 to)
    {
        while(-carsPanel.transform.localPosition.x + Time.deltaTime * _turnSpeed < to.x)
        {
            carsPanel.transform.localPosition -= Vector3.right * Time.deltaTime * _turnSpeed;
            yield return new WaitForEndOfFrame();
        }
        carsPanel.transform.localPosition = -to.x * Vector3.right;
        _turning = false;
        ChangeButton();
    }
    private IEnumerator TransitionPrev(Vector3 to)
    {
        while (carsPanel.transform.localPosition.x + Time.deltaTime * _turnSpeed < -to.x)
        {
            carsPanel.transform.localPosition -= Vector3.left * Time.deltaTime * _turnSpeed;
            yield return new WaitForEndOfFrame();
        }
        carsPanel.transform.localPosition = to.x * Vector3.left;
        _turning = false;
        ChangeButton();
    }
    private void ChangeButton()
    {
        if (carInfo[_curCar].bought)
        {
            costButton.SetActive(false);
            equipButton.SetActive(true);
        }
        else
        {
            costButton.SetActive(true);
            equipButton.SetActive(false);
            costButton.transform.GetChild(0).GetComponent<Text>().text = carInfo[_curCar].cost.ToString();
        }
    }
    public void Choose()
    {
        if (carInfo[_curCar].bought)
        {
            gameObject.SetActive(false);
        }
        else
        {
            if (true)
            {
                carInfo[_curCar].bought = true;
                ChangeButton();
            }
        }
    }
}
