using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "Tutorial Texts", menuName = "SO/Tutorial Texts", order = 0)]
    public class TutorialTextPrompts : ScriptableObject
    {

    [TextArea(4,5)]public string startString =
        "On this fine day your duty is to serve as many of your alien brethren as possible. Travelers from all over the galaxy converge on this fine establishment to purchase some of the most sought after meals in the universe. +When a patron arrives, help them find an vacant table.";

    [TextArea(4,5)]public string howToSeatCustomers = "To help patrons find their way to a table, simply walk up to them and interact with their group (Spacebar), then find a suitable table and interact with it as well! (Spacebar)";
    
    [TextArea(4,5)]public string alienSeatedString = "Well done, friend! You've successfully seated your first patrons, it's quite likely that they will, at some point, want to order some food. When that happens, take their order and try not to butcher them. Aliens are quite sensitive when it comes to their gastronomical needs.";
    
    [TextArea(4,5)]public string alienTakeOrderString = "I'll be damned, this is one hungry fella. Some dietary needs are quite hard to satisfy, but seeing as we're the finest eatery in the galaxy we strive to always be prepared! Be careful that you don't choose a meal that the alien is allergic to, they will tell you what they can't eat when you take their order. +(Use 1,2,3 on your keyboard to select a dish from the list.)";

    [TextArea(4,5)]public string orderTakenString = "Hopefully we managed to satisfy their dietary needs, we really couldn't afford another lawsuit.. Now let's scurry over to the kitchen and hand them the order, the quicker the better so that the chef's don't get overwhelmed!";

    [TextArea(4,5)]public string orderLeftAtKitchen =
        "Fantastic work! Now the chef is cooking that delicious grub. Once the food is ready it will be delivered to the counter!";

    [TextArea(4,5)]public string foodAtTheCounterString = "Although the chefs can be quite salty at times, their cooking abilities are top-notch when it comes to intergalactic dining. Be sure to deliver the order to the correct customer, post-haste!";

    [TextArea(4,5)]public string alienRecievedFoodString = "Wow, you made it, my bet was that you'd quit before you managed to deliver your first order. Stay vigilant, some of these aliens will really test your patience!";

    }
}