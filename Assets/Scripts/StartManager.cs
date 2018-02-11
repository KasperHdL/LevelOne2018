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
    public float gameStartTime = 4.0f;
    private int gameStartCountdown = 3;
    private int currentCount = 0;
    private float gameStartCount;
    private CountdownArgs countdownArgs = new CountdownArgs();

    public bool gameCountdownStarted = false;
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
                    uiManager.PlayerAdded(teamIndex, p, materials[players.Count % materials.Count]);

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
        if(!gameStarted && inputDevices.Count > 1)
        {
            bool pressCheck = false;
            for(int i = 0; i < inputDevices.Count; i++){
                if(!gameCountdownStarted && inputDevices[i].Command.WasPressed){

                    lookForControllers = false;

                    gameCountdownStarted = true;
                    gameStartCount = 0.0f;
                    currentCount = 0;
                }

                if (gameCountdownStarted && inputDevices[i].Command.IsPressed)
                {
                    pressCheck = true;
                    gameStartCount += Time.deltaTime;

                    if (gameStartCount > currentCount)
                    {
                        currentCount++;
                        countdownArgs.count = gameStartCountdown - currentCount;
                        GameEventHandler.TriggerEvent(GameEvent.GameCountdown, countdownArgs);
                    }

                    //check for start
                    if (gameStartCount >= gameStartTime)
                    {
                        gameStarted = true;
                        
                        //Start Game
                        GameStartedArgs args = new GameStartedArgs();
                        args.players = players.ToArray();
                        args.teams = teams.ToArray();
                        
                        GameEventHandler.TriggerEvent(GameEvent.GameStarted, args);
                        Destroy(gameObject);
                    }

                    continue;
                }
            }

            if (gameCountdownStarted && !pressCheck)
            {
                gameCountdownStarted = false;
            }
        }
	}
}
