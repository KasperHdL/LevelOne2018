using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InControl;

public class UIManager : MonoBehaviour {

	[Header("References")]
	public List<Player> players;
	public List<Team> team;

	[Header("UI")]
	public Text[] jumpText;
	public Text[] dashText;
	public Text[] pushText;

	public Image[] jumpImage;
	public Image[] dashImage;
	public Image[] pushImage;
	

	public GameObject[] uiPlayerContainers;

	public Sprite[] buttonSprites;
	public InputControlType startIndex;


	public void GameStarted(GameEventArgs eventArgs){
		GameStartedArgs args = (GameStartedArgs) eventArgs;
	}

	public void PlayerAdded(Player player){
		Debug.Log("Player " + player.id + " Added");
		players.Add(player);

		uiPlayerContainers[player.id].SetActive(true);
	}

	public void ButtonRemapped(int teamIndex, int playerIndex, InputControlType button){

	}
}
