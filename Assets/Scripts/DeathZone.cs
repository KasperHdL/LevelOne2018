using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
	public RoundManager roundManager;

	private PlayerEventArgument playerArgument = new PlayerEventArgument();

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player"){
			roundManager.PlayerDeath(other.gameObject.GetComponent<Player>());

			playerArgument.gameObject = other.gameObject;
			GameEventHandler.TriggerEvent(GameEvent.PlayerDeath, (GameEventArgs)playerArgument);
		}
	}
}
