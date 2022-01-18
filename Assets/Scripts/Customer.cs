using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    [SerializeField] private ScriptableTableSeater tableSeater;

    private Camera cam;
    private NavMeshAgent nmagent;
    private SphereCollider sCollider;

    private bool closeToHost = false;

    private void Start()
    {
        cam = Camera.main;
        nmagent = gameObject.GetComponent<NavMeshAgent>();
        sCollider = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        //If close and mouse is pressed.. (this should be separated into another script..)
        if (Input.GetKeyDown(KeyCode.Space) && closeToHost)
        {
            tableSeater.currentCustomer = this;
        }
        //ClickToAssignTable();
    }

    public void MoveToTable(Vector3 pos)
    {
        nmagent.destination = pos;
        sCollider.radius = 0.5f; //So we can keep assigning to the table
    }

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

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement pmovMovement))
        {
            closeToHost = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        closeToHost = false;
    }
}
