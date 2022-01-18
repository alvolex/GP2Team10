using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class TableAssigner : MonoBehaviour
{
    [SerializeField] private ScriptableTableSeater tableSeater;
    private bool closeToTable = false;

    private Table curTable;

    private void Start()
    {
        tableSeater.currentCustomer = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (tableSeater.currentCustomer != null && Input.GetKeyDown(KeyCode.Space) && closeToTable)
        {
            Vector3 chairPos = curTable.GetEmptyChairPosition();
            tableSeater.currentCustomer.MoveToTable(chairPos);
            tableSeater.currentCustomer = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent == null) return;

        if (other.transform.parent.TryGetComponent<Table>(out Table table))
        {
            closeToTable = true;
            curTable = table;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        closeToTable = false;
        curTable = null;
    }
}
