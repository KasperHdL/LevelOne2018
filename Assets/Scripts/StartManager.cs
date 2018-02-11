using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class StartManager : MonoBehaviour {
    public UIManager uiManager;
    public GameObject[] playerPrefab;
    public List<Player> players;
    public List<Team> teams;
    public List<Material> materials;

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

                int teamIndex = (inputDevices.Count-1) / 2;
                GameObject g = Instantiate(playerPrefab[teamIndex % playerPrefab.Length]) as GameObject;
                g.name = "Player " + players.Count;
                g.SetActive(false);

                Player p = g.GetComponent<Player>();
                p.Initialize();
                p.id = players.Count;
                p.SetMaterial(materials[players.Count % materials.Count]);
                players.Add(p);

                if(uiManager != null)
                    uiManager.PlayerAdded(p, materials[players.Count % materials.Count]);

                if(inputDevices.Count % 2 == 0){
                    //create team

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
                            players[teamIndex * 2 + 1],
                            uiManager
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
