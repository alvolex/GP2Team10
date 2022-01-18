using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "TableSeater", menuName = "TableSeater", order = 0)]
    public class ScriptableTableSeater : ScriptableObject
    {
        public Customer currentCustomer;
        
    }
}