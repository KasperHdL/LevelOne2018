using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class Team : MonoBehaviour {
    public int teamIndex;

    public Player[] players;

    public ActionMap defaultMapping;
    public InputDevice[] inputDevices;

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

        playerMaps = new ActionMap[2] 
        {
            Instantiate(defaultMapping),
            Instantiate(defaultMapping),
        };
    }

    public InputControl GetActionState(int playerId, Action action){
        InputControlType type = playerMaps[playerId].controlType[(int) action];

        bool onMyController = playerMaps[playerId].onMyController[(int) action];
        int controllerIndex = playerId + (onMyController ? 0 : 1) % 2;

        return inputDevices[controllerIndex].GetControl(type);
    }


    public TwoAxisInputControl GetLeftStick(int playerId){
        return inputDevices[playerId % 2].LeftStick;
    }
    public TwoAxisInputControl GetRightStick(int playerId){
        return inputDevices[playerId % 2].RightStick;
    }
    public TwoAxisInputControl GetDPad(int playerId){
        return inputDevices[playerId % 2].DPad;
    }


}
