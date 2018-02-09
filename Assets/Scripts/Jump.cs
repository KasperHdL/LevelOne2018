using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {

	private Rigidbody _playerRigidbody;
	[SerializeField] private float _jumpForce = 5;
	private bool canJump = true;
	void Start ()
	{
		_playerRigidbody = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		if (Input.GetKey(KeyCode.Space) && canJump)
		{
			_playerRigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
			canJump = false;
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		canJump = true;
	}
}
