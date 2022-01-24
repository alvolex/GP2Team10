using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [Header("Setup")] 
    [SerializeField] private int numberOfCustomersToSpawn = 3;
    [SerializeField] private float timeBetweenCustomers = 7f;

    [Header("Spawn probabilities (should add up to 100)(%)")]
    [SerializeField] private int chanceOf1Customer = 25;
    [SerializeField] private int chanceOf2Customers = 25;
    [SerializeField] private int chanceOf3Customers = 25;
    [SerializeField] private int chanceOf4Customers = 25;

    [Header("References")]
    [SerializeField] private Transform doorPos;
    [SerializeField] private GameObject customer;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject groupParentPrefab;
    
    
    void Start()
    {
        StartCoroutine(SpawnCustomers());
    }

    private int CustomersInParty()
    {
        int getRandNumber = Random.Range(0, 100);
        int customersToSpawn = 0;

        if (getRandNumber <= chanceOf1Customer)
            customersToSpawn = 1;
        else if (getRandNumber <= chanceOf1Customer + chanceOf2Customers)
            customersToSpawn = 2;
        else if (getRandNumber <= chanceOf1Customer + chanceOf2Customers + chanceOf3Customers)
            customersToSpawn = 3;
        else if (getRandNumber <= chanceOf1Customer + chanceOf2Customers + chanceOf3Customers + chanceOf4Customers)
            customersToSpawn = 4;

        Debug.Log(getRandNumber + " Cust: " + customersToSpawn);
        return customersToSpawn;
    }

    IEnumerator SpawnCustomers()
    {
        int index = 0;
        for (int i = 0; i < numberOfCustomersToSpawn; i++)
        {
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

        //Create the customer group
        for (int j = 0; j < customersInGroup; j++)
        {
            //todo can this be made prettier?
            //Create customer instance
            GameObject customerInstance = Instantiate(customer, doorPos.position, Quaternion.identity);
            customerInstance.transform.LookAt(player.transform.position);
            customerInstance.name = $"Customer{j}";
            customerInstance.transform.parent = parent.transform; //Assign to parent
            customerInstance.GetComponent<Customer>().restaurantExit = doorPos; //Inject reference to where the exit is located
        }
    }
}
