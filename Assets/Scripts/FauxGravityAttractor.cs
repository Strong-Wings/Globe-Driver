using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityAttractor : MonoBehaviour {
    public float gravity = -10;

	private SphereCollider col;

    public void Start()
    {
		col = GetComponent<SphereCollider>();
    }
    public void Attract(Rigidbody body)
	{
		Vector3 gravityUp = (body.position - transform.position).normalized;
		body.AddForce(gravityUp * gravity);

		RotateBody(body);
	}

	void RotateBody(Rigidbody body)
	{
		Vector3 gravityUp = (body.position - transform.position).normalized;
		Quaternion targetRotation = Quaternion.FromToRotation(body.transform.up, gravityUp) * body.rotation;
		body.MoveRotation(Quaternion.Slerp(body.rotation, targetRotation, 50f * Time.deltaTime));
	}
}
