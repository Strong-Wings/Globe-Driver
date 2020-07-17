using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotationSpeed = 10f;

    private float rotation;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rotation = Input.GetAxisRaw("Horizontal");
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
