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
    private Rigidbody _rb;
    public GameObject gemsAmountField;
    private UnityEngine.UI.Text textField;
    public static bool StartGame;

    private void Start()
    {
        textField = gemsAmountField.GetComponent<UnityEngine.UI.Text>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rotation = Input.GetAxisRaw("Horizontal");   //ЗАКОММЕНТИТЬ ПРИ БИЛДЕ
        /*        if (Input.acceleration.x < -0.1f)
                    rotation = -1;
                else if (Input.acceleration.x > 0.1f)        //РАСКОММЕНТИТЬ ПРИ БИЛДЕ
                    rotation = 1;
                else
                    rotation = 0;*/
        if (StartGame)
        {
            textField.text = gemAmount.ToString();
            StreamWriter balancedataW = new StreamWriter(Application.persistentDataPath + "/balance.gd");
            balancedataW.Write(gemAmount);
            balancedataW.Close();
        }
    }
    private void FixedUpdate()
    {
        if (StartGame)
        {
            if (Input.GetKey(KeyCode.Space)) //ЗАКОММЕНТИТЬ ПРИ БИЛДЕ
                _rb.MovePosition(_rb.position + transform.forward * moveSpeed * Time.fixedDeltaTime); //УБРАТЬ ОТСТУП ПРИ БИЛДЕ
            Vector3 yRotation = Vector3.up * rotation * rotationSpeed * Time.fixedDeltaTime;
            Quaternion deltaRotation = Quaternion.Euler(yRotation);
            Quaternion targetRotation = _rb.rotation * deltaRotation;
            _rb.MoveRotation(Quaternion.Slerp(_rb.rotation, targetRotation, 50f * Time.deltaTime));
        }
    }



}
