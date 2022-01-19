using UnityEngine.Events;

namespace ScriptableEvents
{
    public class ScriptableEventListenerInt : ScriptableEventListenerBase<int, ScriptableEventInt,UnityEventInt>
    {
    }
    public class UnityEventInt : UnityEvent<int>
    {
    }
}