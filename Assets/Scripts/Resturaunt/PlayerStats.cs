using System.Collections;
using System.Collections.Generic;
using ScriptableEvents;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Score: ")] 
    [SerializeField] private IntVariable _points;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private ScriptableEventInt OnPointsChangedEvent;
    
}