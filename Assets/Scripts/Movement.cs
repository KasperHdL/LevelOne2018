using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	private Rigidbody _playerRigidbody;
	[SerializeField] private float _movementForce = 5;
	void Start ()
	{
		_playerRigidbody = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		Vector3 movementdirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		movementdirection = movementdirection.normalized;
		Quaternion lookRot = Quaternion.LookRotation(movementdirection);
		_playerRigidbody.transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.fixedDeltaTime * 30);
		_playerRigidbody.AddForce(movementdirection * _movementForce, ForceMode.Acceleration);
	}
}
