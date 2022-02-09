using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "Tutorial Text", menuName = "SO/Tutorial/Text", order = 0)]
    public class ScriptableTutorialText : ScriptableObject
    {
        [SerializeField, TextArea(4, 5)] private string tutorialText = "This is the text that will be displayed in the tutorial prompt.";
        [SerializeField] bool shouldMoveLights = false;
        
        public bool hasBeenPlayed = false;

        public string TutorialText => tutorialText;
    }
}