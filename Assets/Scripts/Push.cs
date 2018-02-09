using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField] private float _pushForce = 30;
    [SerializeField] private float _pushDistance = 20;
    private Rigidbody _playerRigidbody;

    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        RaycastHit[] hits;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 nearestForward = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;
            //_playerRigidbody.AddForce(nearestForward * _dashForce, ForceMode.Impulse);
            Physics.RaycastAll(transform.position, nearestForward, _pushDistance);

            for( int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];
                Rigidbody body = hit.transform.GetComponent<Rigidbody>();

                if (body)
                {
                    body.AddForce(nearestForward * _dashForce, ForceMode.Impulse);
                }
            }
        }
    }
}
