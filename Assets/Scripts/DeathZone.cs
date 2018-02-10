using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
	public RoundManager roundManager;

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player"){
			roundManager.PlayerDeath(other.gameObject.GetComponent<Player>());
		}
	}
}
