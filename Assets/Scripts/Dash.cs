using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
	[SerializeField] private float _dashForce = 50;
	private Rigidbody _playerRigidbody;

	private void Start()
	{
		_playerRigidbody = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		if (Input.GetAxis("Dash") > 0)
		{
			Vector3 nearestForward = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;
			_playerRigidbody.AddForce(nearestForward * _dashForce, ForceMode.Impulse);
		}
	}
}
