using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.IO;
using System.Linq;

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
            cars[i].transform.localScale = carInfo[i].model.transform.localScale;
            cars[i].transform.localPosition = carInfo[i].model.transform.localPosition + Vector3.right * i * offset;
            cars[i].transform.localRotation = carInfo[i].model.transform.localRotation;
        }
        _curCar = 0;
        carsPanel.transform.localPosition = new Vector3(-cars[_curCar].transform.localPosition.x, 0, 0);
        GetBoughtCars();
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
        cars[_curCar].transform.localRotation = carInfo[_curCar].model.transform.localRotation;
        _curCar++;
        StartCoroutine(TransitionNext(cars[_curCar].transform.localPosition));
    }
    public void PrevCar()
    {
        if (_turning || _curCar == 0) return;
        _turning = true;
        cars[_curCar].transform.localRotation = carInfo[_curCar].model.transform.localRotation;
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
            StreamWriter carData = new StreamWriter(Application.persistentDataPath + "/curCar.gd");
            carData.Write(_curCar);
            carData.Close();
            gameObject.SetActive(false);
        }
        else
        {
            if (PlayerController.gemAmount >= carInfo[_curCar].cost)
            {
                StreamWriter balancedataW = new StreamWriter(Application.persistentDataPath + "/balance.gd");
                balancedataW.Write(PlayerController.gemAmount - carInfo[_curCar].cost);
                balancedataW.Close();
                PlayerController.gemAmount -= carInfo[_curCar].cost;
                PlayerController.textField.text = PlayerController.gemAmount.ToString();
                StreamReader boughtCarsData = new StreamReader(Application.persistentDataPath + "/boughtCars.gd");
                string boughtCars = boughtCarsData.ReadLine();
                boughtCarsData.Close();

                carInfo[_curCar].bought = true;
                char[] ch = boughtCars.ToCharArray();
                ch[_curCar] = '1'; // index starts at 0!
                string newBoughtCars = new string (ch);
                
                StreamWriter wData = new StreamWriter(Application.persistentDataPath + "/boughtCars.gd");
                wData.Write(newBoughtCars);
                wData.Close();
                
                ChangeButton();
            }
        }
    }
    private void GetBoughtCars() {    
        if (!System.IO.File.Exists(Application.persistentDataPath + "/boughtCars.gd")) 
        {
            StreamWriter balancedataW = new StreamWriter(Application.persistentDataPath + "/boughtCars.gd");
            balancedataW.Write("1" + String.Concat(Enumerable.Repeat("0", cars.Length - 1)));
            balancedataW.Close();
        }
        StreamReader boughtCarsData = new StreamReader(Application.persistentDataPath + "/boughtCars.gd");
        string boughtCars = boughtCarsData.ReadLine();
        boughtCarsData.Close();

        for (int i = 0; i < cars.Length; i++)
        {
            if (boughtCars[i] == '1')
                carInfo[i].bought = true;
            else 
                carInfo[i].bought = false;
        }
    }
}
