using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class SaveSystem
{
   public static void SavePlayerStats(PlayerStats playerStats)
   {
      BinaryFormatter formatter = new BinaryFormatter();
      string path = Application.persistentDataPath + "/player.save";
      FileStream stream = new FileStream(path, FileMode.Create);

      PlayerData data = new PlayerData(playerStats);
      
      formatter.Serialize(stream,data);
      stream.Close();
      

   }
   public static PlayerStats LoadPlayer()
   {
      string path = Application.persistentDataPath + "/player.save";
      if (File.Exists(path))
      {
         BinaryFormatter formatter = new BinaryFormatter();
         FileStream stream = new FileStream(path, FileMode.Open);

         PlayerStats stats = formatter.Deserialize(stream) as PlayerStats;
         stream.Close();
         return stats;
      }
      else
      {
         Debug.Log("Savefile not found");
         return null;
      }
   }
    


}
