using UnityEngine;

public class GameEventArgs{
}

public class ExampleArgs : GameEventArgs{
    public int someInt;
}

public class GameStartedArgs : GameEventArgs{
    public Player[] players;
    public Team[] teams;
}

public class PlayerEventArgument : GameEventArgs{
    public Vector3 position;
    public GameObject gameObject;
}