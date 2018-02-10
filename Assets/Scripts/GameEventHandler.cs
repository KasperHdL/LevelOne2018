using UnityEngine;

public class GameEventHandler {
	
	public delegate void Event(GameEventArgs args);

	private static Event[] eventList;
    private static bool[] eventPermanent;

	[RuntimeInitializeOnLoadMethod]
	public static void Initialize()
	{
		eventList = new Event[(int)GameEvent.Count];
        eventPermanent = new bool[(int)GameEvent.Count];

        Reset();
    }       

    public static void Reset()
	{
		for (int i = 0; i < eventList.Length; i++)
		{
            eventList[i] = null;
		}
	}

    public static void TriggerEvent(GameEvent type){TriggerEvent(type, null);}
	public static void TriggerEvent(GameEvent type, GameEventArgs args)
	{
		if(eventList[(int) type] != null){
			eventList[(int) type](args);
		}else{
			//no one is subscribed
		}
	}

	public static void Subscribe(GameEvent gameEvent, Event function)
	{
		eventList[(int)gameEvent] += function;
	}

	public static void Unsubscribe(GameEvent gameEvent, Event function)
	{
		eventList[(int)gameEvent] -= function;
	}
}

