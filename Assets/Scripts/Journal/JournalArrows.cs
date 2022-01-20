using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalArrows : MonoBehaviour
{

    [SerializeField] private GameObject[] journalPages;
    [SerializeField] private Button arrowRight;
    [SerializeField] private Button arrowLeft;

    private int currentPage = 0;

    [SerializeField] private GameObject journal;
    private bool journalState;
    
    
    void Start()
    {
        arrowRight.onClick.AddListener(NextPage);
        arrowLeft.onClick.AddListener(PreviousPage);
    }

    void NextPage()
    {
        journalPages[currentPage].SetActive(false);
        journalPages[currentPage+1].SetActive(true);
        currentPage++;
        
       
        if (currentPage+1 >= journalPages.Length)
        {
            arrowRight.gameObject.SetActive(false);
        }
        if (currentPage > 0)
        {
            arrowLeft.gameObject.SetActive(true);
        }
    }
    void PreviousPage()
    {
        journalPages[currentPage].SetActive(false);
        journalPages[currentPage-1].SetActive(true);
        currentPage--;
        
        
        if (currentPage - 1 < 0)
        {
            arrowLeft.gameObject.SetActive(false);
            
        }if (currentPage+1 <= journalPages.Length)
        {
            arrowRight.gameObject.SetActive(true);
        }
        
    }
}
