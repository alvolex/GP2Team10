using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableEvents
{
    
    [CreateAssetMenu(fileName = "new ScriptableEventInt", menuName = "ScriptableObject/ScriptableEvent-Int", order = 0)]
    public class ScriptableEventInt : ScriptableEvent<int>
    {
        
    }
    public class ScriptableEventVector3 : ScriptableEvent<Vector3>
    {
        
    }
}
