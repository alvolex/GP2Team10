using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Customer : MonoBehaviour
{
    [Header("Scriptable Objects")]
    [SerializeField] private ScriptableTableSeater tableSeater;
    
    //todo should we just change this to reside in a static class so we dont have to pull it into every script that needs it
    [SerializeField] private ScriptablePlayerCurrentAction currentAction;

    [Header("Debug stuff")]
    //Debug stuff that needs to be moved to a better location later..
    public Transform restaurantExit;
    //End debug shenanigans

    private Camera cam;
    private NavMeshAgent nmagent;
    private NavMeshObstacle nmObstacle;
    private SphereCollider sCollider;
    private OrderFood orderFood;
    
    private bool closeToHost = false;
    private Rigidbody rb;

    private bool isMovingToTable;
    private bool hasFinishedEating = false;
    private bool isSeated = false;

    public event Action<Customer> OnFinishedEating;

    private void Start()
    {
        isSeated = false;
        isMovingToTable = false;
        //Get components
        cam = Camera.main;
        orderFood = GetComponent<OrderFood>();
        nmagent = GetComponent<NavMeshAgent>();
        nmObstacle = GetComponent<NavMeshObstacle>();
        sCollider = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleCustomerSelection();
        HandleMovingToTable();
    }

    private void HandleMovingToTable()
    {   //Check if customer has reached the table or not
        if (!isMovingToTable) return;

        if (nmagent.pathPending ||
            nmagent.pathStatus == NavMeshPathStatus.PathInvalid ||
            nmagent.path.corners.Length == 0 || nmagent.remainingDistance >= 0.1f) return;

        isMovingToTable = false; //Not moving if we have reached table
        isSeated = true;

        //Start the food ordering process
        orderFood.Order();
        orderFood.SCollider = sCollider;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        nmagent.ResetPath();
        nmagent.enabled = false;
        nmObstacle.enabled = true;
    }
    
    //todo maybe the player should handle this(?)
    private void HandleCustomerSelection()
    {
        if (Input.GetKeyDown(KeyCode.Space) && closeToHost && !isSeated)
        {
            //Set the current customer to be this customer when selected in our scriptable object
            currentAction.CurrentAction = CurrentAction.SeatingCustomer;
            tableSeater.CurrentCustomer = this;
        }
    }

    public void MoveToTable(Vector3 pos)
    {
        isMovingToTable = true;
        
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        nmagent.destination = pos;
        sCollider.radius = 0.5f; //So we can keep assigning to the table
    }

    //If we ever want to be able to assign a customer to a table without going to it
    //Should work without any changes, just call it when a customer is selected and then click a table
    private void ClickToAssignTable()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && closeToHost)
        {
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity))
            {
                if (hit.transform.parent == null) return;

                if (hit.transform.parent.TryGetComponent(out Table table))
                {
                    Vector3 chairPos = table.GetEmptyChairPosition(tableSeater.CurrentCustomer);
                    if (chairPos != Vector3.positiveInfinity)
                    {
                        nmagent.destination = chairPos;
                        sCollider.radius = 0.5f; //So we can keep assigning to the table
                    }
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (currentAction.CurrentAction == CurrentAction.None && other.TryGetComponent(out PlayerMovement _))
        {
            closeToHost = true;
        }

        //Just testin'
        if (other.CompareTag("Finish") && hasFinishedEating)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        closeToHost = false;
    }

    private void ExitRestaurant()
    {
        rb.constraints = RigidbodyConstraints.None;
        nmObstacle.enabled = false;
        nmagent.enabled = true;
        sCollider.radius = 0.5f;

        nmagent.destination = restaurantExit.transform.position;
    }

    //todo Debugging, should be split into it's own class later
    public void StartEatingFood()
    {
        StartCoroutine(EatFood());
    }
    
    //todo Maybe this should be in it's own class(?)
    IEnumerator EatFood()
    {
        yield return new WaitForSeconds(Random.Range(5f,10f));
        
        //Have to do this otherwise we get pushed when we re-activate the navmesh agent..
        nmObstacle.size = Vector3.zero; 
        yield return null;
        
        hasFinishedEating = true;
        OnFinishedEating?.Invoke(this);
        
        ExitRestaurant();
    }
}
