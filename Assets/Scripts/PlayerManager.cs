using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using InControl;

public class GameManager : MonoBehaviour {

    public List<InputDevice> devices;

    public int maxPlayers = 4;

	void Start () {
        devices = new List<InputDevice>(4);

	}
	
	void Update () {
		
        InputDevice inputDevice = InputManager.ActiveDevice;

        if (inputDevice.AnyButton)
        {
            if (!devices.Contains(inputDevice))
            {
                devices.Add(inputDevice);
            }
        }

	}
}
