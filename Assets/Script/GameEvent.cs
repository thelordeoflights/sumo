using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "Events/Game Event")]
public class GameEvent : ScriptableObject
{

    private List<IGameEventListener> listeners = new List<IGameEventListener>();

    public void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; --i)
        {
            listeners[i].OnEventRaised();
        }
    }

    public void RegisterListener(IGameEventListener listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterListener(IGameEventListener listener)
    {
        listeners.Remove(listener);
    }
}