using System;
using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "ScriptableMoneyPopupEvent", menuName = "SO/Events/Scriptable Money-popup Event", order = 0)]
    public class ScriptableMoneyPopupEvent : ScriptableObject
    {
        public event Action<int, Customer> ScriptableEvent;

        public void InvokeEvent(int money, Customer customer)
        {
            ScriptableEvent?.Invoke(money, customer);
        }
    }
}