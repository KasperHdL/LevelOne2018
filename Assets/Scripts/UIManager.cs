using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InControl;
using MutateOrDie;

public class UIManager : MonoBehaviour {

	public List<Player> players;
	public List<Team> team;

	[Header("UI")]
	public Text[] jumpText;
	public Text[] dashText;
	public Text[] pushText;

	public Image[] jumpImage;
	public Image[] dashImage;
	public Image[] pushImage;
	

	public UIPlayer[] playerObjs;
	public GameObject[] playerUIs;

	public Sprite[] buttonSprites;
	public InputControlType startIndex;

	public Team.ActionMap startActionMap;

	public List<Color> colors;


	public void GameStarted(GameEventArgs eventArgs){
		GameStartedArgs args = (GameStartedArgs) eventArgs;
	}

	public void PlayerAdded(int teamIndex, Player player, Material mat){
		Debug.Log("Player " + player.id + " Added");
		players.Add(player);

		playerObjs[player.id].SetMaterial(mat);
		playerObjs[player.id].gameObject.SetActive(true);

		playerUIs[player.id].SetActive(true);


		colors.Add(mat.color);

		for(int i = 0; i < (int) Action.Count; i++){
			SetButton(player.id, (Action) i, startActionMap.onMyController[i], startActionMap.controlType[i]);
		}

	}


	public void SetButton(int playerIndex, Action action, bool onMyController, InputControlType button){
		int endIndex;

		if(playerIndex % 2 == 0) 
			endIndex = playerIndex + (onMyController ? 0 : 1);
		else
			endIndex = playerIndex - (onMyController ? 0 : 1);

		Sprite buttonSprite = buttonSprites[(int)button - (int)startIndex];

		switch(action){
			case Action.Jump:
				jumpText[playerIndex].color = colors[endIndex];
				jumpImage[playerIndex].sprite = buttonSprite;
			break;
			case Action.Push:
				pushText[playerIndex].color = colors[endIndex];
				pushImage[playerIndex].sprite = buttonSprite;
			break;
			case Action.Dash:
				dashText[playerIndex].color = colors[endIndex];
				dashImage[playerIndex].sprite = buttonSprite;
			break;
		}
	}
}
