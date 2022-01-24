using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "TableSeater", menuName = "SO/TableSeater", order = 0)]
    public class ScriptableTableSeater : ScriptableObject
    {
        [SerializeField] private Customer currentCustomer;
        [SerializeField] private List<Customer> selectedCustomerList;

        public Customer CurrentCustomer
        {
            get => currentCustomer;
            set
            {
                currentCustomer = value;
                OnCurrentCustomerChanged?.Invoke();
            } 
        }

        public void AddCustomerToList(Customer customerToAdd)
        {
            selectedCustomerList.Add(customerToAdd);
        }

        //Action that gets invoked whenever the current customer is changed. 
        //Currently it's just updating some text UI to show which customer is selected
        public event Action OnCurrentCustomerChanged;
    }
}