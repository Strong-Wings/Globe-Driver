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

    private void Start()
    {
        textField = gemsAmountField.GetComponent<UnityEngine.UI.Text>();
        _rb = GetComponent<Rigidbody>();
        
        if (!System.IO.File.Exists(Application.persistentDataPath + "/balance.gd"))
        {
            StreamWriter balancedataW = new StreamWriter (Application.persistentDataPath + "/balance.gd");
            gemAmount = 0;
            balancedataW.Write(0);
            balancedataW.Close();
        }
        else 
        {
            StreamReader balancedata = new StreamReader (Application.persistentDataPath + "/balance.gd");
            gemAmount = int.Parse(balancedata.ReadLine());
            balancedata.Close();
        }
    }

    private void Update()
    {
        rotation = Input.GetAxisRaw("Horizontal");

        textField.text = gemAmount.ToString();
        StreamWriter balancedataW = new StreamWriter (Application.persistentDataPath + "/balance.gd");
        balancedataW.Write(gemAmount);
        balancedataW.Close();
    }
    private void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.Space))
            _rb.MovePosition(_rb.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
        Vector3 yRotation = Vector3.up * rotation * rotationSpeed * Time.fixedDeltaTime;
        Quaternion deltaRotation = Quaternion.Euler(yRotation);
        Quaternion targetRotation = _rb.rotation * deltaRotation;
        _rb.MoveRotation(Quaternion.Slerp(_rb.rotation, targetRotation, 50f * Time.deltaTime));
    }
}
