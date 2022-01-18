using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class CustomerSelectedUI : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private ScriptableTableSeater tableSeater;

    void Start()
    {
        tableSeater.OnCurrentCustomerChanged += UpdateCurrentCustomerUI;
    }

    void UpdateCurrentCustomerUI()
    {
        text.text = $"Customer selected: {tableSeater?.CurrentCustomer}";
    }
}
