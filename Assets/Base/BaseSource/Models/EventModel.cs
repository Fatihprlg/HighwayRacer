using UnityEngine.Events;

[System.Serializable]
public class EventModel : UnityEvent { }

[System.Serializable]
public class GameStateEventModel : UnityEvent<GameStates> { }

[System.Serializable]
public class IntEventModel : UnityEvent<int> { }


[System.Serializable]
public class BoolEventModel : UnityEvent<bool> { }


[System.Serializable]
public class LongEventModel : UnityEvent<long>
{

}