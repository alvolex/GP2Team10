using UnityEngine;
using Variables;

namespace ScriptableEvents
{
    [CreateAssetMenu(fileName = "new ScriptableEventInt", menuName = "ScriptableObject/ScriptableEvent-IntRef", order = 0)]
    public class ScriptableEventIntReference : ScriptableEvent<IntReference>
    {
        public void Raise(int newValue)
        {
            Raise(new IntReference(newValue));
        }
    }
}