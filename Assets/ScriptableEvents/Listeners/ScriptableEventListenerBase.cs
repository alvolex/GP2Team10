using UnityEngine;
using UnityEngine.Events;

namespace ScriptableEvents
{
    public class ScriptableEventListenerBase : MonoBehaviour
    {
        [SerializeField] private ScriptableEvent _eventNoPayload;
        [SerializeField] private UnityEvent _responseNoPayload;
        
        
        private void OnEventRaised()
        {
            _responseNoPayload.Invoke();
        }
        private void OnEnable()
        {
            _eventNoPayload.Register(OnEventRaised);
        }

        private void OnDisable()
        {
            _eventNoPayload.UnRegister(OnEventRaised);
        }
    }

    public class ScriptableEventListenerBase<TPayload,TEvent,TUnityEventResponse> : ScriptableEventListenerBase
        where TEvent : ScriptableEvent<TPayload>
        where TUnityEventResponse : UnityEvent<TPayload>
    {
        [SerializeField] private TEvent _event;
        [SerializeField] private TUnityEventResponse _response;
        
        private void OnEventRaised(TPayload payload)
        {
            _response.Invoke(payload);
        }
        private void OnEnable()
        {
            _event.Register(OnEventRaised);
        }
        private void OnDisable()
        {
            _event.UnRegister(OnEventRaised);
        }
        
    }
}