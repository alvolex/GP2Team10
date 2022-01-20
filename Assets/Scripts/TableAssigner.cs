using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class TableAssigner : MonoBehaviour
{
    [SerializeField] private ScriptableTableSeater tableSeater;
    [SerializeField] private ScriptablePlayerCurrentAction currentAction;

    [SerializeField]private Table curTable;
    
    private bool closeToTable = false;
    private Collider tableCollider;

    private void Start()
    {
        tableSeater.CurrentCustomer = null;
        currentAction.CurrentAction = CurrentAction.None; //todo this is a pretty dirty place to put this...
    }
    
    void Update()
    {
        if (!closeToTable || curTable == null) return;

        if (!CheckIfTableIsInRange()) return;

        AssignCustomerToTable();
    }

    private bool CheckIfTableIsInRange()
    {
        //Get the closest point of the table collider and see if that position is further than 2f in this case
        Vector3 closestPoint = tableCollider.ClosestPoint(transform.position);
        if (Vector3.Distance(transform.position, closestPoint) <= 2f)
        {
            return true;
        }

        //Unhighlight and reset table data if we're too far from the table
        if (curTable != null && closeToTable)
        {
            curTable.UnhighlightTable();
        }
        closeToTable = false;
        curTable = null;
        tableCollider = null;

        return false;
    }

    private void AssignCustomerToTable()
    {
        if (tableSeater.CurrentCustomer != null && Input.GetKeyDown(KeyCode.Space) && closeToTable)
        {
            //Pass in who the customer who will sit in a specific chair so we can keep track when they leave
            Vector3 chairPos = curTable.GetEmptyChairPosition(tableSeater.CurrentCustomer);
            currentAction.CurrentAction = CurrentAction.None;

            //Move customer to the assigned chair
            tableSeater.CurrentCustomer.MoveToTable(chairPos);
            tableSeater.CurrentCustomer = null;
            
            //Unhighlight and reset table info when a customer has been assigned
            curTable.UnhighlightTable();
            closeToTable = false;
            curTable = null;
            tableCollider = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent == null || tableSeater.CurrentCustomer == null) return;
        tableCollider = other;

        if (other.transform.parent.TryGetComponent<Table>(out Table table))
        {
            table.HighlightTable();
            
            closeToTable = true;
            curTable = table;
        }
    }

    /*private void OnTriggerExit(Collider other)
    {
        if (curTable != null && closeToTable)
        {
            curTable.UnhighlightTable();
        }
        closeToTable = false;
        curTable = null;
    }*/

}
