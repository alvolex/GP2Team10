using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalToggle : MonoBehaviour
{
    [SerializeField] private GameObject journal;
    private bool journalState;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            journalState = journal.activeSelf;

            journalState = !journalState;
            
            journal.SetActive(journalState);
            
            if (!journal.activeSelf)
            {
                AudioManager.Instance.PlayJournalCloseSFX();
            }
            else if (journal.activeSelf)
            {
                AudioManager.Instance.PlayJournalOpenSFX();
            }
        }
    }
}
