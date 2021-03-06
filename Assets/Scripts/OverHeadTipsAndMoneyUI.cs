using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableEvents;
using Scriptables;
using TMPro;
using UnityEngine;

public class OverHeadTipsAndMoneyUI : MonoBehaviour
{
    [SerializeField] private GameObject moneyPopupUI;
    //[SerializeField] private GameObject reputationPopupUI;
    
    
    [SerializeField] private TMP_Text tmpMoneyText;
    //[SerializeField] private TMP_Text tmpRepText;

    [Header("Event")] 
    [SerializeField] private ScriptableMoneyPopupEvent moneyEvent;
    //[SerializeField] private ScriptableMoneyPopupEvent repEvent;
    private Customer myself;

    private void Start()
    {
        myself = GetComponent<Customer>();
        moneyEvent.ScriptableEvent += HandleMoneyPopup;
        //repEvent.ScriptableEvent += HandleRepPopup;
    }

    /*private void HandleRepPopup(int arg1, Customer arg2)
    {
        if (myself == null || arg2 == null) return;
        if (arg2.gameObject.GetInstanceID() != myself.gameObject.GetInstanceID()) return;

        moneyPopupUI.SetActive(true);
        tmpRepText.text = $"+${arg1}";

        StartCoroutine(SlowlySlideUpRep(reputationPopupUI));
    }*/

    private void HandleMoneyPopup(int moneyRecieved, Customer customer)
    {
        if (myself == null || customer == null) return;
        if (customer.gameObject.GetInstanceID() != myself.gameObject.GetInstanceID()) return;

        moneyPopupUI.SetActive(true);
        tmpMoneyText.text = $"+${moneyRecieved}";

        StartCoroutine(SlowlySlideUp(moneyPopupUI));
    }

    IEnumerator SlowlySlideUp(GameObject UI)
    {
        Vector3 startVector = UI.transform.position;
        
        float startTime = Time.time;

        var startpos = moneyPopupUI.transform.position.y;
        var endPos = UI.transform.position.y + 3f;
        var t = 0f;
        
        while (startTime + 1.5f > Time.time)
        {
            yield return null;

            t += 0.2f * Time.deltaTime;
            var y = Mathf.Lerp(startpos, endPos, t);

            var position = UI.transform.position;
            position = new Vector3(position.x, y, position.z);
            UI.transform.position = position;
        }

        UI.transform.position = startVector;
        UI.SetActive(false);
    }
    /*IEnumerator SlowlySlideUpRep(GameObject UI)
    {
        Vector3 startVector = UI.transform.position;
        
        float startTime = Time.time;

        var startpos = reputationPopupUI.transform.position.y;
        var endPos = UI.transform.position.y + 3f;
        var t = 0f;
        
        while (startTime + 1.5f > Time.time)
        {
            yield return null;

            t += 0.2f * Time.deltaTime;
            var y = Mathf.Lerp(startpos, endPos, t);

            var position = UI.transform.position;
            position = new Vector3(position.x, y, position.z);
            UI.transform.position = position;
        }

        UI.transform.position = startVector;
        UI.SetActive(false);
    }*/
    
}
