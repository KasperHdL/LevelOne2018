using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventExample : MonoBehaviour {

	void Start () {
		GameEventHandler.Subscribe(GameEvent.Example, Trigger);
	}
	void Trigger(GameEventArgs eventArgs){
		ExampleArgs args = eventArgs as ExampleArgs;
		//do something with args
		print(args.someInt);
	}
	
	
}
