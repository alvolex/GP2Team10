using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class TableAssigner : MonoBehaviour
{
    [SerializeField] private ScriptableTableSeater tableSeater;
    private bool closeToTable = false;

     [SerializeField]private Table curTable;

    private void Start()
    {
        tableSeater.CurrentCustomer = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (tableSeater.CurrentCustomer != null && Input.GetKeyDown(KeyCode.Space) && closeToTable)
        {
            //Pass in who the customer who will sit in a specific chair so we can keep track when they leave
            Vector3 chairPos = curTable.GetEmptyChairPosition(tableSeater.CurrentCustomer);
            
            tableSeater.CurrentCustomer.MoveToTable(chairPos);
            tableSeater.CurrentCustomer = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent == null) return;

        if (other.transform.parent.TryGetComponent<Table>(out Table table))
        {
            table.HighlightTable();
            
            closeToTable = true;
            curTable = table;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (curTable != null)
        {
            curTable.UnhighlightTable();
        }
        closeToTable = false;
        curTable = null;
    }

}
