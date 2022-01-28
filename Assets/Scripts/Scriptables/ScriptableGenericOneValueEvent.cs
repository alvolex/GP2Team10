using System;
using UnityEngine;

namespace Scriptables
{
    //Base event   
    public class ScriptableGenericOneValueEvent<T> : ScriptableObject
    {
        public event Action<T> ScriptableEvent;

        public void InvokeEvent(T value)
        {
            ScriptableEvent?.Invoke(value);
        }
    }
}