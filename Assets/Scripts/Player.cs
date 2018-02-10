using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using InControl;

public class Player : MonoBehaviour {
    public int id; 

	public Rigidbody body;
    private Animator animator;

    public PlayerSettings settings;
    
    private bool onGround = true;
    private bool pushOnCooldown;
    private bool dashOnCooldown;

    private float nextDash;
    private float nextPush;
    private float currentDashTime;
    private bool isDashing;
    private PlayerEventArgument playerArgument = new PlayerEventArgument();

    //temp should use team to get remappings
    public Team team;

    private LayerMask groundMask;

	public void Initialize()
	{
        playerArgument.gameObject = gameObject;
		body = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        groundMask = LayerMask.GetMask("Ground");
	}

	private void Update(){
        if(team == null){
            return;
        }

        // Reset animation values
        animator.SetBool("Jump", false);
        animator.SetBool("Dashing", false);
        animator.SetBool("Push", false);

        if(Physics.Raycast(transform.position, Vector3.down, settings.distanceFromGround, groundMask))
        {
            if (!onGround)
            {
                GameEventHandler.TriggerEvent(GameEvent.Land, (GameEventArgs)playerArgument);
            }
            onGround = true;
        }

        Vector2 input = team.GetLeftStick(id).Value;

        if (!isDashing)
        {
            if(input.magnitude > 0.25f){
                Vector3 movementdirection = new Vector3(input.x, 0, input.y);
                movementdirection = movementdirection.normalized * input.magnitude;

                Quaternion lookRot = Quaternion.LookRotation(movementdirection);
                transform.rotation = lookRot;

                float force = (onGround ? settings.movementForce : settings.airForce);
                
                if (body.velocity.magnitude < settings.movementVelocity)
                {
                    body.AddForce(movementdirection * force * Time.deltaTime);
                }
            }
        }

        //Jump
        if(onGround){
            if(team.GetActionState(id, Action.Jump).WasPressed){
                body.AddForce(Vector3.up * settings.jumpForce, ForceMode.Impulse);
                onGround = false;

                GameEventHandler.TriggerEvent(GameEvent.Jump, (GameEventArgs)playerArgument);
                animator.SetBool("Jump", true);
            }
        }

        Vector3 nearestForward = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;

        //Dash
        if(nextDash < Time.time){
            if (dashOnCooldown)
            {
                dashOnCooldown = false;
                GameEventHandler.TriggerEvent(GameEvent.CooldownDash, (GameEventArgs)playerArgument);
            }

            if(team.GetActionState(id, Action.Dash).WasPressed)
            {
                nextDash = Time.time + settings.dashDelay;
                
                dashOnCooldown = true;
                isDashing = true;
                currentDashTime = 0.0f;
                GameEventHandler.TriggerEvent(GameEvent.Dash, (GameEventArgs)playerArgument);
                animator.SetBool("Dashing", true);
            }
        }

        if (isDashing && currentDashTime < settings.dashTime)
        {
            currentDashTime += Time.deltaTime;
            Debug.Log(body.velocity.magnitude);

            if (body.velocity.magnitude < settings.dashVelocity)
            {
                body.AddForce(nearestForward * settings.dashForce * Time.deltaTime);
            }
        } else if (isDashing)
        {
            isDashing = false;
        }

        //Push

        RaycastHit[] hits;

        if (nextPush < Time.time)
        {
            if (pushOnCooldown)
            {
                pushOnCooldown = false;
                GameEventHandler.TriggerEvent(GameEvent.CooldownPush, (GameEventArgs)playerArgument);
            }

            if (team.GetActionState(id, Action.Push).WasPressed)
            {
                nextPush = Time.time + settings.pushDelay;
                pushOnCooldown = true;

                GameEventHandler.TriggerEvent(GameEvent.Push, (GameEventArgs)playerArgument);
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
                        
                        if(angle < settings.pushDegrees){
                            hitBody.AddForce(dot * direction * settings.pushForce, ForceMode.Impulse);
                        }
                    }
                }
                animator.SetBool("Push", true);
            }
        }

        // Animation
        animator.SetFloat("Speed", body.velocity.magnitude);
	}
    
    void OnCollisionEnter(Collision other)
    {
        if (isDashing && other.gameObject.GetComponent<Player>() != null)
        {
            int rnd = Random.Range(0, other.contacts.Length -1);
            playerArgument.position = other.contacts[rnd].point;
            GameEventHandler.TriggerEvent(GameEvent.PlayerHit, (GameEventArgs)playerArgument);
        }
    }
}
