using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FauxGravityBody : MonoBehaviour {

	public FauxGravityAttractor attractor;
	
	private Rigidbody _rb;

	void Start()
	{
		_rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		attractor.Attract(_rb);
	}

}
