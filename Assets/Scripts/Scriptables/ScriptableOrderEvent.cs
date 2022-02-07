using DefaultNamespace;
using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "Simple Order Event", menuName = "SO/Events/Generic Order Event", order = 0)]
    public class ScriptableOrderEvent : ScriptableGenericOneValueEvent<Order>
    {
    }
}