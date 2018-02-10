using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
	public RoundManager roundManager;

	void OnTriggerEnter(Collider other)
	{
		print(other);
		print(other.gameObject);
		roundManager.PlayerDeath(other.GetComponent<Player>());
	}
}
