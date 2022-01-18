using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    [SerializeField] private ScriptableTableSeater tableSeater;

    //Debug stuff that needs to be moved to a better location later..
    public Transform restaurantExit;
    //End debug shenanigans

    private Camera cam;
    private NavMeshAgent nmagent;
    private SphereCollider sCollider;
    
    private bool closeToHost = false;
    private Rigidbody rb;

    private bool isMovingToTable;
    private bool hasFinishedEating = false;

    private void Start()
    {
        isMovingToTable = false;
        cam = Camera.main;
        nmagent = gameObject.GetComponent<NavMeshAgent>();
        sCollider = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleCustomerSelection();
        HandleMovingToTable();
    }

    private void HandleMovingToTable()
    {
        if (!isMovingToTable) return;

        //Check if customer has reached the table or not
        if (nmagent.pathPending ||
            nmagent.pathStatus == NavMeshPathStatus.PathInvalid ||
            nmagent.path.corners.Length == 0 || nmagent.remainingDistance >= 0.1f) return;

        //Not moving if we have reached table
        isMovingToTable = false;

        StartCoroutine(EatFood());
    }

    private void HandleCustomerSelection()
    {
        if (Input.GetKeyDown(KeyCode.Space) && closeToHost)
        {
            //Set the current customer to be this customer when selected in our scriptable object
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
                    Vector3 chairPos = table.GetEmptyChairPosition();
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
        if (other.TryGetComponent(out PlayerMovement _))
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

    IEnumerator EatFood()
    {
        yield return new WaitForSeconds(5f);

        hasFinishedEating = true;
        nmagent.destination = restaurantExit.transform.position;
    }
    
}
