using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class StartManager : MonoBehaviour {
    public GameObject playerPrefab;
    public List<Player> players;
    public List<Team> teams;

    public GameObject teamPrefab;

    public List<InputDevice> inputDevices;

    public bool gameStarted = false;
    public bool lookForControllers = true;

	void Start () {
        inputDevices = new List<InputDevice>(4);
	}
	
	void Update () {
        if(lookForControllers){
            InputDevice activeDevice = InputManager.ActiveDevice;

            if(activeDevice.AnyButtonIsPressed && !inputDevices.Contains(activeDevice)){
                inputDevices.Add(activeDevice);

                GameObject g = Instantiate(playerPrefab) as GameObject;
                g.name = "Player " + players.Count;
                g.SetActive(false);
                Player p = g.GetComponent<Player>();
                p.Initialize();
                p.id = players.Count;
                players.Add(p);

                if(inputDevices.Count % 2 == 0){
                    //create team

                    int teamIndex = (inputDevices.Count-1) / 2;

                    g = Instantiate(teamPrefab) as GameObject;
                    Team t = g.GetComponent<Team>();
                    teams.Add(t);

                    players[teamIndex * 2].team = t;
                    players[teamIndex * 2 + 1].team = t;

                    t.Initialize(
                            teamIndex, 
                            inputDevices[teamIndex * 2], 
                            inputDevices[teamIndex * 2 + 1],
                            players[teamIndex * 2],
                            players[teamIndex * 2 + 1]
                        );
                }
            }
        }
        if(!gameStarted && inputDevices.Count > 1){

            //check for start
            for(int i = 0; i < inputDevices.Count; i++){
                if(inputDevices[i].Command.WasPressed){
                    //Start Game
                    lookForControllers = false;
                    gameStarted = true;

                    GameStartedArgs args = new GameStartedArgs();
                    args.players = players.ToArray();
                    args.teams = teams.ToArray();

                    GameEventHandler.TriggerEvent(GameEvent.GameStarted, args);
                    Destroy(gameObject);
                }
            }
        }
	}
}
