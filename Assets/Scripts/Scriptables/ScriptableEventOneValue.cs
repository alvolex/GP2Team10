using System;
using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "ScriptableValueEvent", menuName = "SO/Events/Scriptable Value Event", order = 0)]
    public class ScriptableEventOneValue : ScriptableObject
    {
        public event Action<int> ScriptableEvent;

        public void InvokeEvent(int value)
        {
            ScriptableEvent?.Invoke(value);
        }
    }
}