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

        public List<Customer> SelectedCustomerList => selectedCustomerList;

        public Customer CurrentCustomer
        {
            get => currentCustomer;
            set
            {
                currentCustomer = value;
                OnCurrentCustomerChanged?.Invoke();
            } 
        }

        public void AssignToList(List<Customer> customerList)
        {
            selectedCustomerList = customerList;
        }

        public void ClearCustomerList()
        {
            selectedCustomerList = new List<Customer>();
        }

        //Action that gets invoked whenever the current customer is changed. 
        //Currently it's just updating some text UI to show which customer is selected
        public event Action OnCurrentCustomerChanged;
    }
}