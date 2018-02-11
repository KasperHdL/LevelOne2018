using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour 
{
	private Rigidbody rb;
	public float forceMultiplier;
	public float speedMultiplier;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		Vector3 force = new Vector3(Mathf.Sin(Time.time * speedMultiplier), 0, 0) * forceMultiplier;
		rb.AddRelativeTorque(force);
	}
}
