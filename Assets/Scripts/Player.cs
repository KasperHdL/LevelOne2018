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


    //temp should use team to get remappings
    public Team team;

    private LayerMask groundMask;

	void Start ()
	{
		body = GetComponent<Rigidbody>();
        groundMask = LayerMask.GetMask("Ground");
	}

	private void Update(){
        if(team == null){
            return;
        }

        onGround = Physics.Raycast(transform.position, Vector3.down, settings.distanceFromGround, groundMask);

        Vector2 input = team.GetLeftStick(id).Value;

        if(input != Vector2.zero){
            Vector3 movementdirection = new Vector3(input.x, 0, input.y);
            movementdirection = movementdirection.normalized;

            Quaternion lookRot = Quaternion.LookRotation(movementdirection);
            transform.rotation = lookRot;

            float force = (onGround ? settings.movementForce : settings.airForce);
            body.AddForce(movementdirection * force * Time.deltaTime);
        }

        //Jump
        if(onGround){
            if(team.GetActionState(id, Action.Jump).WasPressed){
                body.AddForce(Vector3.up * settings.jumpForce, ForceMode.Impulse);
            }
        }

        Vector3 nearestForward = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;

        //Dash
        if(nextDash < Time.time){
            if(team.GetActionState(id, Action.Dash).WasPressed){
                body.AddForce(nearestForward * settings.dashForce, ForceMode.Impulse);
                nextDash = Time.time + settings.dashDelay;
            }
        }

        //Push

        RaycastHit[] hits;

        if (team.GetActionState(id, Action.Push).WasPressed){
            hits = Physics.SphereCastAll(transform.position, settings.pushDistance, nearestForward, 0.0001f);

            for( int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];

                Rigidbody hitBody = hit.transform.GetComponent<Rigidbody>();

                if (hitBody && hitBody != body)
                {
                    Vector3 direction = Vector3.ProjectOnPlane(hitBody.transform.position - transform.position, Vector3.up).normalized;
                    float dot = Vector3.Dot(direction, nearestForward);
                    float angle = Mathf.Rad2Deg * Mathf.Acos(dot);
                    
                    if(angle < settings.pushDegrees && hitBody != body){
                        hitBody.AddForce(dot * direction * settings.pushForce, ForceMode.Impulse);
                    }
                }
            }
        }
	}
}
