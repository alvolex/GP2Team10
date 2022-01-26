using System;
using UnityEngine;

namespace DefaultNamespace
{
    /// <summary>
    /// This will be responsible for keeping track of the players current action so that the player can only interact
    /// with certain things in the restaurant.
    /// Example:
    /// Current action is seating a customer, we can only interact with tables during this time.
    /// </summary>

    public enum CurrentAction
    {
        SeatingCustomer,
        TakingOrder,
        HandlingOrder,
        DeliveringFood,
        None
    }

    [CreateAssetMenu(fileName = "PlayerCurrentAction", menuName = "SO/PlayerCurrentAction", order = 0)]
    public class ScriptablePlayerCurrentAction : ScriptableObject
    {
        [SerializeField] private CurrentAction currentAction = CurrentAction.None;

        public CurrentAction CurrentAction
        {
            get => currentAction;
            set
            {
                currentAction = value;
                OnCurrentActionChanged?.Invoke();
            } 
        }

        //Action that gets invoked whenever the current customer is changed. 
        //Currently it's just updating some text UI to show which customer is selected
        public event Action OnCurrentActionChanged;
        
    }
}