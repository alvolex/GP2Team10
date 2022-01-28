using System;
using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "SimpleScriptableEvent", menuName = "SO/Events/Simple Scriptable Event", order = 0)]
    public class ScriptableSimpleEvent : ScriptableObject
    {
        public event Action ScriptableEvent;

        public void InvokeEvent()
        {
            ScriptableEvent?.Invoke();
        }
    }
}