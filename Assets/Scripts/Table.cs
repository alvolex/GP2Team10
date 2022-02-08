using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Table : MonoBehaviour
{
    [Header("Table settings")] 
    [SerializeField] private bool canSeatManyGroups;
    [SerializeField]private bool isUnlocked;

    [Header("References")]
    [SerializeField] private List<GameObject> chairPositions;
    [SerializeField] private MeshRenderer tableMeshRenderer;
    [SerializeField] private Collider triggerCollider;
    [SerializeField] private Transform centerOftable;
    [SerializeField] private Outline outline;
    

    [Header("Materials")]
    [SerializeField] private Material defaultMat;
    [SerializeField] private Material selectedMat;
    [SerializeField] private Material lockedMaterial;
    [Header("Debug")]
    [SerializeField]private int emptyChairs = 0;

    private Dictionary<Customer, int> customerChairDictionary = new Dictionary<Customer, int>();
    private Dictionary<int, bool> availableSeats = new Dictionary<int, bool>();

    public Transform CenterOftable => centerOftable;
    public bool IsUnlocked => isUnlocked;

    private void Start()
    {
        outline = GetComponent<Outline>();
        emptyChairs = chairPositions.Count;
        SetupAvailableChairs();
        SetCorrectMaterial();
    }

    private void SetCorrectMaterial()
    {
        tableMeshRenderer.material = isUnlocked ? defaultMat : lockedMaterial;
    }

    public bool IsEmpty()
    {
        if (canSeatManyGroups) return true;

        return emptyChairs == chairPositions.Count;
    }

    public void UnlockTable()
    {
        isUnlocked = true;
        tableMeshRenderer.material = defaultMat;
    }

    public void ToggleSeatingTrigger()
    {
        triggerCollider.gameObject.SetActive(!triggerCollider.gameObject.activeSelf);
    }

    private void SetupAvailableChairs()
    {
        for (int i = 0; i < chairPositions.Count; i++)
        {
            availableSeats.Add(i, true);
        }
    }

    public int NumberOfEmptyChairs()
    {
        return emptyChairs;
    }

    public void HighlightTable()
    {
        //tableMeshRenderer.material = selectedMat;
        outline.OutlineOn();
    }
    
    public void UnhighlightTable()
    {
        //tableMeshRenderer.material = defaultMat;
        outline.OutlineOff();
    }

    public bool HasEmptySeat()
    {
        return emptyChairs != 0;
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
        if (customerChairDictionary.ContainsKey(customer))
        {
            emptyChairs++;
            availableSeats[customerChairDictionary[customer]] = true;
            customerChairDictionary.Remove(customer);
        }
    }
}
