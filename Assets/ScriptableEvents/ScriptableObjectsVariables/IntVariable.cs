using UnityEngine;


[CreateAssetMenu(fileName = "new IntVariable", menuName = "ScriptableObject/Variables/IntVariable")]
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