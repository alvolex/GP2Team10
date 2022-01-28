using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "Simple Float Event", menuName = "SO/Events/Generic Float Event", order = 0)]
    public class ScriptableFloatEvent : ScriptableGenericOneValueEvent<float>
    {
    }
}