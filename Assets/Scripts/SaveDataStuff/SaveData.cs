using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    private static SaveData _current;
    public static SaveData current
    {
        get
        {
            if (current == null)
            {
                _current = new SaveData();
            }

            return current;
        }
        set => _current = value;
    }

    public PlayerData data;

    public List<AlienData> aliens;


}
