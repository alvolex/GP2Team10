using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class PlayerStateHandler : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button restartGameButton;
    [SerializeField] private Button quitToMenuButton;
    
    
    public Object mainMenu;


    public void Start()
    {
        restartGameButton.gameObject.SetActive(false);
        quitToMenuButton.gameObject.SetActive(false);
    }


    public void YouLost()
    {
        
        restartGameButton.gameObject.SetActive(true);
        quitToMenuButton.gameObject.SetActive(true);
        
        Debug.Log("You lose lol");
        Time.timeScale = 0;
    }
    
    

    public void RestartGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        Time.timeScale = 1;
        SceneManager.LoadScene(scene.name);
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(mainMenu.name);
    }
}
