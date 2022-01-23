using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField] private List<GameObject> chairPositions;
    [SerializeField] private MeshRenderer tableMeshRenderer;

    [SerializeField] private Material defaultMat;
    [SerializeField] private Material selectedMat;

    [SerializeField]private int emptyChairs = 0;

    private Dictionary<Customer, int> customerChairDictionary = new Dictionary<Customer, int>();
    private Dictionary<int, bool> availableSeats = new Dictionary<int, bool>();
    
    private void Start()
    {
        emptyChairs = chairPositions.Count;
        SetupAvailableChairs();
    }

    private void SetupAvailableChairs()
    {
        for (int i = 0; i < chairPositions.Count; i++)
        {
            availableSeats.Add(i, true);
        }
    }

    public void HighlightTable()
    {
        tableMeshRenderer.material = selectedMat;
    }
    
    public void UnhighlightTable()
    {
        tableMeshRenderer.material = defaultMat;
    }

    public Vector3 GetEmptyChairPosition(Customer customer)
    {
        if (emptyChairs != 0)
        {
            //Empty the chair 
            customer.OnFinishedEating += HandleCustomerFinishedEating;
            customer.GetComponent<AlienAttributes>().customerHasDied += HandleCustomerFinishedEating;
            
            Vector3 posToReturn = Vector3.zero;

            if (availableSeats.ContainsValue(true)) //If true then there is an available seat
            {
                var myKey = availableSeats.FirstOrDefault(x => x.Value == true).Key; //Get key of first available seat

                posToReturn = chairPositions[myKey].transform.position;
                    
                customerChairDictionary.Add(customer,myKey);
                availableSeats[myKey] = false;
            }

            emptyChairs--;
            return posToReturn;
        }
        
        return Vector3.positiveInfinity;
    }

    private void HandleCustomerFinishedEating(Customer customer)
    {
        emptyChairs++;

        if (customerChairDictionary.ContainsKey(customer))
        {
            availableSeats[customerChairDictionary[customer]] = true;
            customerChairDictionary.Remove(customer);
        }
    }
}
