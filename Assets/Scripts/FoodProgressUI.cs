using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class FoodProgressUI : MonoBehaviour
    {
        [SerializeField] private List<GameObject> uiGameobject = new List<GameObject>();
        [SerializeField] private List<Image> imageList = new List<Image>();

        [SerializeField] private Sprite test;
        [SerializeField] private float testFloat;
        
        

        private Dictionary<GameObject, Image> combinedList = new Dictionary<GameObject, Image>();

        private void Start()
        {
            //ListsToDict();
            UpdateFoodImageAndProgress(test, testFloat);
        }

        private void ListsToDict()
        {
            for (int i = 0; i < uiGameobject.Count; i++)
            {
                combinedList.Add(uiGameobject[i], imageList[i]);
            }
        }

        public void UpdateFoodImageAndProgress(Sprite foodSprite, float timeToCook)
        {
            var index = FindFirstFreeUI();
            Debug.Log(index);
            
            Debug.Log(uiGameobject[index]);
            
            if (index != -1)
            {
                Debug.Log("called?");
                uiGameobject[index].SetActive(true);
                uiGameobject[index].GetComponent<Image>().sprite = test;
                
                
                //imageList[index].sprite = test;

                StartCoroutine(HandleUpdateUI(testFloat));
            }
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

        IEnumerator HandleUpdateUI(float timeToCook)
        {
            yield return null;
        }
        
    }
}