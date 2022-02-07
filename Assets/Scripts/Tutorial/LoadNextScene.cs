using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{

    public void LoadScene()
    {
        Time.timeScale = 1;
        Destroy(Tutorial.instance);
        SceneManager.LoadScene("MAIN_Alpha");
        
    }
}
