using System;
using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "Game State", menuName = "SO/GameState", order = 0)]
    public class ScriptableGameState : ScriptableObject
    {
        public bool shouldShowTutorial = true;
        
        //Tutorial pop-up checks
        public bool hasBeenSeatedTutorial;
        public bool hasStartedSeatingCustomer;
        public bool howToTakeOrderTutorial;
        public bool hasTakenOrderTutorial;
        public bool hasLeftOrderAtKitchenTutorial;
        public bool foodReadyToDeliverTutorial;
        public bool foodPickedUpFromCounter;
        public bool alienReceivedFoodTutorial;

        //Reset checks on startup
        private void OnEnable()
        {
            hasBeenSeatedTutorial = true;
            hasStartedSeatingCustomer = true;
            howToTakeOrderTutorial = true;
            hasTakenOrderTutorial = true;
            hasLeftOrderAtKitchenTutorial = true;
            foodReadyToDeliverTutorial = true;
            foodPickedUpFromCounter = true;
            alienReceivedFoodTutorial = true;
        }
        
        public void ResetAll()
        {
            shouldShowTutorial = true;
            hasBeenSeatedTutorial = true;
            hasStartedSeatingCustomer = true;
            howToTakeOrderTutorial = true;
            hasTakenOrderTutorial = true;
            hasLeftOrderAtKitchenTutorial = true;
            foodReadyToDeliverTutorial = true;
            foodPickedUpFromCounter = true;
            alienReceivedFoodTutorial = true;
        }
    }
}