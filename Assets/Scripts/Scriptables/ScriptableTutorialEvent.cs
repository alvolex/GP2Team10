using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "Simple Tutorial Event", menuName = "SO/Events/Generic Tutorial Event", order = 0)]
    public class ScriptableTutorialEvent : ScriptableGenericOneValueEvent<int>
    {
    }
}