using System;
using UnityEngine;

namespace ScriptableEvents
{
    [CreateAssetMenu(fileName = "new ScriptableEvent", menuName = "ScriptableObject/ScriptableEvent", order = 0)]
    public abstract class ScriptableEventBase : ScriptableObject
    {
        private event Action _eventNoPayload;
        public void Register(Action onEventNoPayload)
        {
            _eventNoPayload += onEventNoPayload;
        }
        public void UnRegister(Action onEventNoPayload)
        {
            _eventNoPayload -= onEventNoPayload;
        }

        public void Raise()
        {
            _eventNoPayload?.Invoke();
        }
    }

    [CreateAssetMenu(fileName = "new ScriptableEvent", menuName = "ScriptableObject/ScriptableEvent", order = 0)]
    public class ScriptableEvent : ScriptableEventBase
    {
    }

    public abstract class ScriptableEvent<TPayload> : ScriptableEventBase
    {
        private event Action<TPayload> _event;
        
        public void Register(Action<TPayload> onEventNoPayload)
        {
            _event += onEventNoPayload;
        }
        public void UnRegister(Action<TPayload> onEventNoPayload)
        {
            _event -= onEventNoPayload;
        }
        
        public void Raise(TPayload newValue)
        {
            _event?.Invoke(newValue);
        }
        
    }
}