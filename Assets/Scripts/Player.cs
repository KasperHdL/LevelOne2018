using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
using InControl;

public class Player : MonoBehaviour {


	private Rigidbody body;
	public float movementForce = 5;

    [Header("Jump")]
	public float jumpForce = 5;
	public float distanceFromGround = .5f;

    [Header("Dash")]
    public float dashDelay;
    public float dashForce;
    private float nextDash;
    
    [Header("Push")]
    public float pushForce = 10;
    public float pushDistance = 20;
    public float pushDegrees = 45;


    //temp should use team to get remappings
    public InputDevice inputDevice;

    private LayerMask groundMask;

	void Start ()
	{
		body = GetComponent<Rigidbody>();
        groundMask = LayerMask.GetMask("Ground");
	}

	private void Update(){

        if(inputDevice == null){
            InputDevice test = InputManager.ActiveDevice;

            if(test.AnyButtonIsPressed){
                Debug.Log("Controller Registered");
                inputDevice = test;
            }

            return;
        }

        Vector2 input = inputDevice.LeftStick.Value;
		Vector3 movementdirection = new Vector3(input.x, 0, input.y);

		movementdirection = movementdirection.normalized;

        if(movementdirection != Vector3.zero){
            Quaternion lookRot = Quaternion.LookRotation(movementdirection);
            body.transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 30);
            body.AddForce(movementdirection * movementForce, ForceMode.Acceleration);
        }

        //Jump

        if(Physics.Raycast(transform.position, Vector3.down, distanceFromGround, groundMask)){
            if(inputDevice.Action1.WasPressed){
                body.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }

        Vector3 nearestForward = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;

        //Dash
        if(nextDash < Time.time){
            if(inputDevice.Action2.WasPressed){
                body.AddForce(nearestForward * dashForce, ForceMode.Impulse);
                nextDash = Time.time + dashDelay;
            }
        }

        //Push

        RaycastHit[] hits;

        if (inputDevice.RightBumper.WasPressed){
            hits = Physics.SphereCastAll(transform.position, pushDistance, nearestForward, 0.0001f);

            for( int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];

                Rigidbody hitBody = hit.transform.GetComponent<Rigidbody>();

                if (hitBody && hitBody != body)
                {
                    Vector3 direction = Vector3.ProjectOnPlane(hitBody.transform.position - transform.position, Vector3.up).normalized;
                    float dot = Vector3.Dot(direction, nearestForward);
                    float angle = Mathf.Rad2Deg * Mathf.Acos(dot);
                    
                    if(angle < pushDegrees && hitBody != body){
                        hitBody.AddForce(dot * direction * pushForce, ForceMode.Impulse);
                    }
                }
            }
        }
	}
=======
public class Player : MonoBehaviour
{
	public int id;
>>>>>>> master
}
