using UnityEngine;

public class GameEventArgs{
}

public class ExampleArgs : GameEventArgs{
    public int someInt;
}

public class PlayerEventArgument : GameEventArgs{
    public Vector3 position;
    public GameObject gameObject;
}
