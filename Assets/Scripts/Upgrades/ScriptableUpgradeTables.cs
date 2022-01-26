using System;
using System.Collections.Generic;
using UnityEngine;

namespace Upgrades
{
    [CreateAssetMenu(fileName = "ScriptableUpdateTable", menuName = "SO/Scriptable Update Table", order = 0)]
    public class ScriptableUpgradeTables : ScriptableObject
    {
        [SerializeField] private List<Table> lockedTables = new List<Table>();

        private void OnEnable()
        {
            //Todo connect to the Unlock Event
        }

        private void UnlockTable()
        {
            if (lockedTables.Count <= 0)
            {
                Debug.Log("No tables to unlock bruh");
                return;
            }
         
            lockedTables[0].UnlockTable();
        }
    }
}