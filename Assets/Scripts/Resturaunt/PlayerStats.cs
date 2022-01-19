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


public class IntVariable : ScriptableObject
{
    
    [SerializeField] private int _value;

    private int _currentValue;

    public int Value => _currentValue;

    public virtual void ApplyChange(int change)
    {
        _currentValue += change;
    }

    public virtual void SetValue(int newValue)
    {
        _currentValue = newValue;
    }

    private void OnEnable()
    {
        _currentValue = _value;
    }
}
