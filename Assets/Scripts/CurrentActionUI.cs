using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class CurrentActionUI : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private ScriptablePlayerCurrentAction currentAction;

    void Start()
    {
        currentAction.OnCurrentActionChanged += UpdateCurrentAction;
        UpdateCurrentAction();
    }

    void UpdateCurrentAction()
    {
        string enumName = Enum.GetName(typeof(CurrentAction), currentAction.CurrentAction);
        text.text = $"Current Action:   {enumName}";
    }
}
