using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotationSpeed = 10f;
    public static int gemAmount;
    private float rotation;
    public GameObject gemsAmountField;
    public GameObject[] otherCars;
    public static UnityEngine.UI.Text textField;
    public static bool StartGame;

    private Rigidbody _rb;

    private void Start()
    {
        textField = gemsAmountField.GetComponent<UnityEngine.UI.Text>();
        _rb = GetComponent<Rigidbody>();
        setCurCar();
    }

    private void Update()
    {
        if (UIManager.pause) return;
        rotation = Input.GetAxisRaw("Horizontal");   //ЗАКОММЕНТИТЬ ПРИ БИЛДЕ
/*        if (Input.acceleration.x < -0.1f)
            rotation = Mathf.Max(5 * Input.acceleration.x, -2.0f);
        else if (Input.acceleration.x > 0.1f)        //РАСКОММЕНТИТЬ ПРИ БИЛДЕ
            rotation = Mathf.Min(2.0f, 5 * Input.acceleration.x);
        else
            rotation = 0;*/
        if (StartGame)
        {
            textField.text = gemAmount.ToString();
            StreamWriter balancedataW = new StreamWriter(Application.persistentDataPath + "/balance.gd");
            balancedataW.Write(gemAmount);
            balancedataW.Close();
        }

        foreach (var car in otherCars)
        {
            car.transform.position = transform.position;
            car.transform.rotation = transform.rotation;
            car.transform.Rotate(new Vector3(180f, 0, 180f));
        }

    }
    private void FixedUpdate()
    {
        if (StartGame && !UIManager.pause)
        {
            if (Input.GetKey(KeyCode.Space)) //ЗАКОММЕНТИТЬ ПРИ БИЛДЕ
                _rb.MovePosition(_rb.position + transform.forward * moveSpeed * Time.fixedDeltaTime); //УБРАТЬ ОТСТУП ПРИ БИЛДЕ
            Vector3 yRotation = Vector3.up * rotation * rotationSpeed * Time.fixedDeltaTime;
            Quaternion deltaRotation = Quaternion.Euler(yRotation);
            Quaternion targetRotation = _rb.rotation * deltaRotation;
            _rb.MoveRotation(Quaternion.Slerp(_rb.rotation, targetRotation, 50f * Time.deltaTime));
            
        }
    }
    
    private void setCurCar() {
        if (!System.IO.File.Exists(Application.persistentDataPath + "/curCar.gd")) {
            StreamWriter carData = new StreamWriter(Application.persistentDataPath + "/curCar.gd");
            carData.Write(0);
            carData.Close();
        } else {
            StreamReader carData = new StreamReader(Application.persistentDataPath + "/curCar.gd");
            int curCar = int.Parse(carData.ReadLine());
            carData.Close();
            
            if (curCar == 0){
                foreach (var oc in otherCars)
                {
                    oc.transform.localScale = new Vector3(0, 1f, 1f);
                }
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else {
                transform.localScale = new Vector3(0f, 1f, 1f);
                for (int i = 0; i < otherCars.Length; i++)
                {
                    if (i + 1 == curCar)
                        otherCars[i].transform.localScale = new Vector3(1f, 1f, 1f);
                    else
                        otherCars[i].transform.localScale = new Vector3(0f, 1f, 1f);
                }
                
            }
        }
    }


}
