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
        public bool howToTakeOrderTutorial;
        public bool hasTakenOrderTutorial;
        public bool hasLeftOrderAtKitchenTutorial;
        public bool foodReadyToDeliverTutorial;
        public bool alienReceivedFoodTutorial;

        //Reset checks on startup
        private void OnEnable()
        {
            hasBeenSeatedTutorial = true;
            howToTakeOrderTutorial = true;
            hasTakenOrderTutorial = true;
            hasLeftOrderAtKitchenTutorial = true;
            foodReadyToDeliverTutorial = true;
            alienReceivedFoodTutorial = true;
        }
    }
}