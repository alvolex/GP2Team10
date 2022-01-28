using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "Simple Int Event", menuName = "SO/Events/Generic Int Event", order = 0)]
    public class ScriptableIntEvent : ScriptableGenericOneValueEvent<int>
    {
    }
}