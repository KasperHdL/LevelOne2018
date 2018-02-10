using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerSettings", menuName = "", order = 1)]
public class PlayerSettings : ScriptableObject {

    [Header("Movement")]
	public float movementForce = 5;
	public float airForce = 5;

    [Header("Jump")]
	public float jumpForce = 5;
	public float distanceFromGround = .5f;

    [Header("Dash")]
    public float dashDelay;
    public float dashForce;
    
    [Header("Push")]
    public float pushForce = 10;
    public float pushDistance = 20;
    public float pushDegrees = 45;
}
