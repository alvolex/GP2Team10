using System;
using System.Collections;
using System.Collections.Generic;
using Scriptables;
using UnityEngine;
using Random = UnityEngine.Random;

public class CustomerSpawner : MonoBehaviour
{
    [Header("Setup")] 
    [SerializeField, Tooltip("This is per day")] private int maxNumberOfCustomersToSpawn = 3;
    [SerializeField, Tooltip("How much *harder* the next day should be")] private int numberOfCustomersPerDayMultiplier = 1;
    [SerializeField, Tooltip("Seconds between customers spawning")] private float timeBetweenCustomers = 7f;

    [Header("Spawn probabilities (should add up to 100)(%)")]
    [SerializeField] private int chanceOf1Customer = 25;
    [SerializeField] private int chanceOf2Customers = 25;
    [SerializeField] private int chanceOf3Customers = 25;
    [SerializeField] private int chanceOf4Customers = 25;

    [Header("References")]
    [SerializeField] private Transform doorPos;
    [SerializeField] private List<GameObject> customers = new List<GameObject>();
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject groupParentPrefab;
    
    [Header("Event")] 
    [SerializeField] private ScriptableSimpleEvent handleStopSpawningCustomers; //Event invoked from DayManager
    [SerializeField] private ScriptableSimpleEvent onNewDaySpawnCustomers; //Event invoked from DayManager
    [SerializeField] private ScriptableEventOneValue customerMovementSpeedChange;
    
    private bool stopCoroutine;
    private int customerSpeed;
    bool isFirstDay = true;
    
    //todo use this to check if the restaurant is empty or not
    private int totalSpawnedCustomersToday = 0;
    //private int customersWhoLeftOrDied = 0;

    void Start()
    {
        StartCoroutine(SpawnCustomers());
        handleStopSpawningCustomers.ScriptableEvent += StopSpawningCustomers;
        onNewDaySpawnCustomers.ScriptableEvent += StartSpawningCustomers;
        stopCoroutine = false;
    }

    private void Awake()
    {
        customerMovementSpeedChange.ScriptableEvent += UpdateCustomerSpeed;
    }

    private void OnDestroy()
    {
        handleStopSpawningCustomers.ScriptableEvent -= StopSpawningCustomers;
        onNewDaySpawnCustomers.ScriptableEvent -= StartSpawningCustomers;
        customerMovementSpeedChange.ScriptableEvent -= UpdateCustomerSpeed;
    }

    void UpdateCustomerSpeed(int value)
    {
        customerSpeed += value;
    }
    

    private void StartSpawningCustomers()
    {
        //Starting to spawn customers because an event told me to
        maxNumberOfCustomersToSpawn += numberOfCustomersPerDayMultiplier;
        stopCoroutine = false;
        StartCoroutine(SpawnCustomers());
    }

    private void StopSpawningCustomers()
    {
        //Stopped spawning customers because an event told me to
        Debug.Log("Total customers today: " + totalSpawnedCustomersToday);
        stopCoroutine = true;
        StopCoroutine(SpawnCustomers());
    }

    private int CustomersInParty()
    {
        int getRandNumber = Random.Range(0, 100);
        int customersToSpawn = 0; //How many customers to spawn in the group

        if (getRandNumber <= chanceOf1Customer)
            customersToSpawn = 1;
        else if (getRandNumber <= chanceOf1Customer + chanceOf2Customers)
            customersToSpawn = 2;
        else if (getRandNumber <= chanceOf1Customer + chanceOf2Customers + chanceOf3Customers)
            customersToSpawn = 3;
        else if (getRandNumber <= chanceOf1Customer + chanceOf2Customers + chanceOf3Customers + chanceOf4Customers)
            customersToSpawn = 4;
            
        return customersToSpawn;
    }

    IEnumerator SpawnCustomers()
    {
        yield return new WaitForSeconds(0.5f);
        int index = 0;
        for (int i = 0; i < maxNumberOfCustomersToSpawn; i++)
        {
            if (stopCoroutine)
            {
                yield break;
            }
            
            int amountOfCustomers = CustomersInParty();
            SpawnCustomer(index, amountOfCustomers);

            i = i + amountOfCustomers - 1;
            index++;
            yield return new WaitForSeconds(timeBetweenCustomers);
        }
    }
    
    void SpawnCustomer(int i, int customersInGroup)
    {
        //Create parent obj
        GameObject parent = Instantiate(groupParentPrefab);
        parent.name = $"Customer_Group{i}";

        totalSpawnedCustomersToday += customersInGroup;

        //Create the customer group
        for (int j = 0; j < customersInGroup; j++)
        {
            //todo can this be made prettier?
            //Create customer instance
            int randomCustomerIndex = Random.Range(0, customers.Count -1);
            
            GameObject customerInstance = Instantiate(customers[randomCustomerIndex], doorPos.position, Quaternion.identity);
            customerInstance.GetComponent<Customer>().ChangeMovementspeed(customerSpeed);
            
            customerInstance.transform.LookAt(player.transform.position);
            customerInstance.name = $"Customer{j}";
            customerInstance.transform.parent = parent.transform; //Assign to parent
            customerInstance.GetComponent<Customer>().restaurantExit = doorPos; //Inject reference to where the exit is located
        }
    }
}
