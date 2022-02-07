using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "Generic Tutorial Event", menuName = "SO/Events/Generic Tutorial Event", order = 0)]
    public class ScriptableTutorialEvent : ScriptableGenericOneValueEvent<ScriptableTutorialText>
    {
    }
}