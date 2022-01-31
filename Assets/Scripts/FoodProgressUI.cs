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
        
        [Header("Events")]
        [SerializeField] private ScriptableSimpleEvent foodPickedUp;

        private Dictionary<GameObject, Image> combinedList = new Dictionary<GameObject, Image>();

        private void Awake()
        {
            foodPickedUp.ScriptableEvent += FoodTakenFromCounter;
        }

        private void ListsToDict()
        {
            for (int i = 0; i < uiGameobject.Count; i++)
            {
                combinedList.Add(uiGameobject[i], progressbarList[i]);
            }
        }

        public void UpdateFoodImageAndProgress(Sprite foodSprite, float timeToCook)
        {
            var index = FindFirstFreeUI();
            
            if (index != -1)
            {
                Debug.Log("called?");
                uiGameobject[index].SetActive(true);
                uiGameobject[index].GetComponent<Image>().sprite = foodSprite;

                StartCoroutine(HandleUpdateUI(index, timeToCook));
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

        IEnumerator HandleUpdateUI(int index, float timeToCook)
        {
            float startTime = 0;
            var curBar =  progressbarList[index];

            while (curBar.fillAmount < 0.99f)
            {
                startTime += Time.deltaTime;
                curBar.fillAmount = startTime / timeToCook;
                yield return null;
            }
        }
        
    }
}