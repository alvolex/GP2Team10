using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class SelectGroupOfCustomers : MonoBehaviour
{
    [SerializeField] private ScriptableTableSeater scriptableTableSeater;

    public void SelectCustomersInGroup()
    {
        List<Customer> customersInGroup = GetComponentsInChildren<Customer>().ToList();
        AddCustomerToList(customersInGroup);
    }
    
    private void AddCustomerToList(List<Customer> customersToAdd)
    {
        scriptableTableSeater.AssignToList(customersToAdd);
    }
}
