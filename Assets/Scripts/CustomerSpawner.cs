using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private int amountOfCustomersToSpawn = 3;
    
    
    [SerializeField] private Transform doorPos;
    [SerializeField] private GameObject customer;
    
    [SerializeField] private GameObject player; //Just out of lazyness atm..

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCustomers());
    }

    IEnumerator SpawnCustomers()
    {
        for (int i = 0; i < amountOfCustomersToSpawn; i++)
        {
            SpawnCustomer(i);
            yield return new WaitForSeconds(7f);
        }
    }

    void SpawnCustomer(int i)
    {
        GameObject customerInstance = Instantiate(customer, doorPos.position, Quaternion.identity);
        customerInstance.transform.LookAt(player.transform.position);
        customerInstance.name = $"Customer_{i}";
        
        //todo This makes me a bit uncomfortable
        customerInstance.GetComponent<Customer>().restaurantExit = doorPos;
    }
}
