using System;
using System.Collections;
using System.Collections.Generic;
using Scriptables;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class FoodProgressUI : MonoBehaviour
    {
        [SerializeField] private List<GameObject> uiGameobject = new List<GameObject>();
        [SerializeField] private List<Image> progressbarList = new List<Image>();
        
        [Header("Hazard pos")] 
        [SerializeField] private HazardManager hazManager;
        [SerializeField] private KitchenThrowFood throwFood;
        [SerializeField] private Transform[] controlPoints;


        [Header("Events")]
        [SerializeField] private ScriptableSimpleEvent foodPickedUp;

        private Dictionary<GameObject, Image> combinedList = new Dictionary<GameObject, Image>();

        private void Awake()
        {
            foodPickedUp.ScriptableEvent += FoodTakenFromCounter;
        }

        private void OnDestroy()
        {
            foodPickedUp.ScriptableEvent -= FoodTakenFromCounter;
        }

        private void ListsToDict()
        {
            for (int i = 0; i < uiGameobject.Count; i++)
            {
                combinedList.Add(uiGameobject[i], progressbarList[i]);
            }
        }

        public void UpdateFoodImageAndProgress(Sprite foodSprite, float timeToCook, float timeBeforFoodGoesBad, Order currentOrder)
        {
            var index = FindFirstFreeUI();
            
            if (index != -1)
            {
                uiGameobject[index].SetActive(true);
                uiGameobject[index].GetComponent<Image>().sprite = foodSprite;

                StartCoroutine(HandleUpdateUI(index, timeToCook, timeBeforFoodGoesBad, currentOrder));
            }
        }

        private void FoodTakenFromCounter()
        {
            var tempGameobject = uiGameobject[0];
            var tempProgressbar = progressbarList[0];
            
            //Remove the object from pos1
            uiGameobject.RemoveAt(0);
            progressbarList.RemoveAt(0);
            
            //Reset the removed objects
            tempGameobject.SetActive(false);
            tempProgressbar.fillAmount = 0;
            tempProgressbar.color = new Color(0.07f, 1f, 0f); //Reset to "base" color
            
            //Add them back to the end of the list
            uiGameobject.Add(tempGameobject);
            progressbarList.Add(tempProgressbar);
            
            //Change sibling position to last
            tempGameobject.transform.SetAsLastSibling();

            //Profit?
        }

        private int FindFirstFreeUI()
        {
            for (int i = 0; i < uiGameobject.Count; i++)
            {
                if (!uiGameobject[i].activeSelf)
                {
                    return i;
                }
            }
            return -1;
        }

        IEnumerator HandleUpdateUI(int index, float timeToCook, float timeBeforFoodGoesBad, Order currentOrder)
        {
            float startTime = 0;
            var curBar =  progressbarList[index];

            //Cooking food progress
            while (curBar.fillAmount < 0.99f)
            {
                startTime += Time.deltaTime;
                curBar.fillAmount = startTime / timeToCook;
                yield return null;
            }
            //Food turning bad
            curBar.fillAmount = 0;
            curBar.color = Color.red;
            startTime = 0;
            
            while (curBar.fillAmount < 0.99f)
            {
                //If the order is in the players hand, it won't go bad(?)
                if (currentOrder.HasBeenPickedUp)
                {
                    yield break;
                }
                
                startTime += Time.deltaTime;
                curBar.fillAmount = startTime / timeBeforFoodGoesBad;
                yield return null;
            }

            //If the order stays to long on the counter it will go bad
            currentOrder.HasSpoiled = true;
            StartCoroutine(ThrowFoodIntoRestaurant(currentOrder));

        }

        IEnumerator ThrowFoodIntoRestaurant(Order currentOrder)
        {
            yield return new WaitForSeconds(5f);
            if (currentOrder.HasBeenPickedUp) yield break;

            //Get hazard positions and set the curve to correct pos
            Vector3 hazPos = hazManager.GetHazardPosition();
            foreach (var point in controlPoints)
            {
                point.position = new Vector3(hazPos.x, point.position.y, hazPos.z);
            }
            throwFood.ThrowFood();
            
            foodPickedUp.InvokeEvent();
        }
    }
}