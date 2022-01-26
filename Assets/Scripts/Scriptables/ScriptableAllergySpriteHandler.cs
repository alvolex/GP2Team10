using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "AllergySpriteHandler", menuName = "SO/Allergy Sprite Handler", order = 0)]
    public class ScriptableAllergySpriteHandler : ScriptableObject
    {
        [SerializeField] private GetAllergySprites getSprites;

        public Sprite GetSprite(Ingredients.Allergy allergy)
        {
            return getSprites.GetSprite(allergy);
        }
    }
}