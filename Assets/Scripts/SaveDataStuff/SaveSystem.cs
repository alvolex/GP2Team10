using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
   /* public static void SavePlayerStats(Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);//SAVED DATA IS HERE
      
        formatter.Serialize(stream,data);
        stream.Close();
      

    }
    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData stats = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return stats;
        }
        else
        {
            Debug.Log("Savefile not found");
            return null;
        }
    } 
    */
    
}
