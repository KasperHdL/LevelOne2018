using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
	public float secondsTillRespawn = 5;
	public int[] playerDeaths;
	public Transform[] spawnPositions;
	
	private Team[] teams;
	private Player[] players;
	private RigidbodyConstraints defaultConstraints;

	void Start()
	{
		GameEventHandler.Subscribe(GameEvent.GameStarted, GameStarted);
	}

	public void GameStarted(GameEventArgs eventArgs){
		GameStartedArgs args = (GameStartedArgs) eventArgs;
		players = args.players;
		teams = args.teams;

		playerDeaths = new int[players.Length];

		int i = 0;
		foreach(Player player in players){
			if(i++ == 0)
				defaultConstraints = player.body.constraints;

			player.transform.position = spawnPositions[player.id].position;
			player.gameObject.SetActive(true);
			playerDeaths[player.id] = 0;
		}
	}

	public void PlayerDeath(Player player){
		if(player.enabled){
			player.enabled = false;
			player.body.constraints = RigidbodyConstraints.None;
			Vector3 r = Random.onUnitSphere;
			r.y = 0;
			player.body.AddTorque(r * player.settings.deathTorque, ForceMode.Impulse);
			StartCoroutine(RespawnCooldown(player));
		}
	}

	private IEnumerator RespawnCooldown(Player player)
	{
		playerDeaths[player.id]++;
		yield return new WaitForSeconds(secondsTillRespawn);
		player.enabled = true;
		player.body.constraints = defaultConstraints;
		player.transform.position = spawnPositions[player.id].position;

		//remap other team
		if(teams.Length == 2)
			teams[(player.team.teamIndex + 1 ) % 2].RemapButton();
		else
			teams[(player.team.teamIndex) % 2].RemapButton();

	}
}
