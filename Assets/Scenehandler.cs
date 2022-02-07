using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Scenehandler : MonoBehaviour
{
    
    
    public Object scene;
    
    
    public void StartGame()
    {
        SceneManager.LoadScene("MAIN_Tutorial");
    }
    

}
