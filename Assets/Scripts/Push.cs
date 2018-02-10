using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    [SerializeField] private float _pushForce = 10;
    [SerializeField] private float _pushDistance = 40;
    [SerializeField] private float _pushDegrees = 30;
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
            hits = Physics.SphereCastAll(transform.position, _pushDistance, nearestForward, 0.0001f);

            for( int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];
                Rigidbody body = hit.transform.GetComponent<Rigidbody>();

                if (body)
                {
                    Vector3 direction = Vector3.ProjectOnPlane(body.transform.position - transform.position, Vector3.up).normalized;
                    float angle = Mathf.Rad2Deg * Mathf.Acos(Vector3.Dot(direction, nearestForward));
                    
                    if(angle < _pushDegrees && body != _playerRigidbody){
                        body.AddForce(direction * _pushForce, ForceMode.Impulse);
                    }
                }
            }
        }
    }
}
