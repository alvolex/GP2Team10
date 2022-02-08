using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NegativeReputationPrompt : MonoBehaviour
{
    
    [SerializeField] private GameObject reputationPopupUI;
    [SerializeField] private TextMeshProUGUI reputationText;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void HandleMoneyPopup(Vector3 pos, int negativeRep)
    {
        reputationText.text = $"-{negativeRep}";
        
        reputationPopupUI.SetActive(true);
        reputationPopupUI.transform.position = pos;
        StartCoroutine(SlowlySlideUp(reputationPopupUI));
    }
    
    
    IEnumerator SlowlySlideUp(GameObject customerPosition)
    {
        Vector3 startVector = customerPosition.transform.position;
        
        float startTime = Time.time;

        var startpos = startVector.y+2;
        var endPos = startpos + 3f;
        var t = 0f;
        
        while (startTime + 1.5f > Time.time)
        {
            yield return null;

            t += 0.2f * Time.deltaTime;
            var y = Mathf.Lerp(startpos, endPos, t);

            var position = customerPosition.transform.position;
            position = new Vector3(position.x, y, position.z);
            customerPosition.transform.position = position;
        }

        customerPosition.transform.position = startVector;
        customerPosition.SetActive(false);
    }
}
