using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class SelectGroupOfCustomers : MonoBehaviour
{
    [SerializeField] private ScriptableTableSeater scriptableTableSeater;

    private List<Customer> customersInGroup = new List<Customer>();

    private void Start()
    {
        customersInGroup = GetComponentsInChildren<Customer>().ToList();
    }

    public void HighlightGroup()
    {
        if (scriptableTableSeater.IsHighlighting) return;

        foreach (var customer in customersInGroup)
        {
            if (customer == null) continue; //If the poor fella has died from being poisoned :(
            customer.HighlightCustomer();
        }

        scriptableTableSeater.IsHighlighting = true;
    }
    
    public void UnhighlightGroup()
    {
        foreach (var customer in customersInGroup)
        {
            if (customer == null) continue; //If the poor fella has died from being poisoned :(
            customer.UnhighlightCustomer();
        }

        scriptableTableSeater.IsHighlighting = false;
    }
    
    public void SelectCustomersInGroup()
    {
        AddCustomerToList(customersInGroup);
    }
    
    private void AddCustomerToList(List<Customer> customersToAdd)
    {
        scriptableTableSeater.AssignToList(customersToAdd);
    }
}
