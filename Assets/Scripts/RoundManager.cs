using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
	public float secondsTillRespawn = 5;
	public int[] playerDeaths;
	public Text playerScoreText;
	public Transform[] spawnPositions;
	
	private Player[] players;

	void Start()
	{
		players = FindObjectsOfType<Player>();
		playerDeaths = new int[players.Length];
		foreach(Player player in players)
		{
			playerDeaths[player.id] = 0;
		}
	}

	public void PlayerDeath(Player player)
	{
		player.gameObject.SetActive(false);
		StartCoroutine(RespawnCooldown(player));
	}

	private IEnumerator RespawnCooldown(Player player)
	{
		playerDeaths[player.id]++;
		yield return new WaitForSeconds(secondsTillRespawn);
		player.gameObject.SetActive(true);
		player.transform.position = spawnPositions[player.id].position;
	}
}
