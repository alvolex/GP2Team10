using System;
using UnityEngine;


namespace Variables
{
    [Serializable]
    public class IntReference
    {
        [SerializeField] private IntVariable _intVariable;
        [SerializeField] private int _simpleValue;
        [SerializeField] private bool _useSimple;
        
        public IntReference(IntVariable variable)
        {
            _intVariable = variable;
            _useSimple = false;
        }
        public IntReference(int value)
        {
            _simpleValue = value;
            _useSimple = true;
        }

        public int GetValue()
        {
            return _useSimple ? _simpleValue : _intVariable.Value;
        }
        public void ApplyChange(int change)
        {
            if (_useSimple)
            {
                _simpleValue += change;
            }
            else
            {
                _intVariable.ApplyChange(change);
            }
        }
    }
}