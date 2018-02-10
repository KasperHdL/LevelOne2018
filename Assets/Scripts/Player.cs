using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using InControl;

public class Player : MonoBehaviour {
    public int id; 

	private Rigidbody body;

    public PlayerSettings settings;
    private bool onGround = true;
    private float nextDash;
    private Vector3 nearestForward;


    //temp should use team to get remappings
    public InputDevice inputDevice;

    private LayerMask groundMask;

	void Start ()
	{
		body = GetComponent<Rigidbody>();
        groundMask = LayerMask.GetMask("Ground");
	}

	private void Update()
    {

        if(inputDevice == null){
            InputDevice test = InputManager.ActiveDevice;

            if(test.AnyButtonIsPressed){
                Debug.Log("Controller Registered");
                inputDevice = test;
            }

            return;
        }

        nearestForward = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;
        onGround = Physics.Raycast(transform.position, Vector3.down, settings.distanceFromGround, groundMask);

        if (inputDevice.LeftStick.Value != Vector2.zero)
        {
            Move();
        }

        //Jump
        if(inputDevice.Action1.WasPressed)
        {
            Jump();
        }

        //Dash
        if(inputDevice.Action2.WasPressed)
        {
            Dash();
        }

        //Push
        if (inputDevice.RightBumper.WasPressed)
        {
            Push();
        }
	}

    private void Move()
    {
        Vector2 input = inputDevice.LeftStick.Value;
        
		Vector3 movementdirection = new Vector3(input.x, 0, input.y);
		movementdirection = movementdirection.normalized;

        Quaternion lookRot = Quaternion.LookRotation(movementdirection);
        body.transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 30);
        float force = (onGround ? settings.movementForce : settings.airForce);
        body.AddForce(movementdirection * force, ForceMode.Acceleration);
    }

    private void Jump()
    {
        if(onGround)
        {
            body.AddForce(Vector3.up * settings.jumpForce, ForceMode.Impulse);
        }
    }

    private void Dash()
    {
        if(nextDash < Time.time)
        {
            body.AddForce(nearestForward * settings.dashForce, ForceMode.Impulse);
            nextDash = Time.time + settings.dashDelay;
        }
    }

    private void Push()
    {
        RaycastHit[] hits;
        hits = Physics.SphereCastAll(transform.position, settings.pushDistance, nearestForward, 0.0001f);

        for( int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];

            Rigidbody hitBody = hit.rigidbody;

            if (hitBody && hitBody != body)
            {
                Vector3 direction = Vector3.ProjectOnPlane(hitBody.transform.position - transform.position, Vector3.up).normalized;
                float dot = Vector3.Dot(direction, nearestForward);
                float angle = Mathf.Rad2Deg * Mathf.Acos(dot);
                
                if(angle < settings.pushDegrees)
                {
                    hitBody.AddForce(dot * direction * settings.pushForce, ForceMode.Impulse);
                }
            }
        }
    }
}
