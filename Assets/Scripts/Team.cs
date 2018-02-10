using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class Team : MonoBehaviour {
    public int teamIndex;

    public Player[] players;

    public ActionMap defaultMapping;
    public InputDevice[] inputDevices;

    public int[] playerIds;

    [CreateAssetMenu(fileName = "ActionMap", menuName = "", order = 1)]
    public class ActionMap : ScriptableObject{
        //layed out so that you can index into with Action cast to int
        public bool[] onMyController;
        public InputControlType[] controlType;

        ActionMap(){
            onMyController = new bool[(int)Action.Count];
            controlType = new InputControlType[(int)Action.Count];
        }
    }

    public ActionMap[] playerMaps;

    public void Initialize(int teamIndex, InputDevice c0, InputDevice c1, Player p0, Player p1){
        this.teamIndex = teamIndex;

        inputDevices = new InputDevice[2] {c0, c1};
        players = new Player[2] {p0, p1};
        playerIds = new int[2] {
            p0.id,
            p1.id,
        };

        playerMaps = new ActionMap[2] 
        {
            Instantiate(defaultMapping),
            Instantiate(defaultMapping),
        };
    }

    public InputControl GetActionState(int playerId, Action action){
        int index = GetIndex(playerId);
        InputControlType type = playerMaps[index].controlType[(int) action];

        bool onMyController = playerMaps[index].onMyController[(int) action];
        int controllerIndex = (index + (onMyController ? 0 : 1)) % 2;

        return inputDevices[controllerIndex].GetControl(type);
    }


    public TwoAxisInputControl GetLeftStick(int playerId){
        int index = GetIndex(playerId);
        return inputDevices[index].LeftStick;
    }
    public TwoAxisInputControl GetRightStick(int playerId){
        int index = GetIndex(playerId);
        return inputDevices[index].RightStick;
    }
    public TwoAxisInputControl GetDPad(int playerId){
        int index = GetIndex(playerId);
        return inputDevices[index].DPad;
    }

    public int GetIndex(int playerId){
        for(int i = 0; i < playerIds.Length; i++){
            if(playerId == playerIds[i]) return i;
        }
        return -1;
    }

    public void RemapButton(){
        //player indices
        int owner = Random.Range(0,2);
        int receiver = (owner + 1) % 2;

        int action = Random.Range(0, (int)Action.Count);
        playerMaps[owner].onMyController[action] = false;

        InputControlType type;
        
        int count = 0;
        do{
            type = (InputControlType) Random.Range((int) InputControlType.LeftTrigger, (int) InputControlType.Action5);
            count ++;
        }while(!IsButtonFree(owner, type) && count < 1000);

        playerMaps[owner].controlType[action] = type;

    }

    public bool IsButtonFree(int index, InputControlType button){
        for(int i = 0; i < playerMaps[index].controlType.Length; i++){
            if(playerMaps[index].controlType[i] == button){
                return false;
            }
        }

        return true;
    }


}
