using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseUI : MonoBehaviour
{
    [SerializeField] private PauseMenu pauseMenu;

    public void RestartGame()
    {
        pauseMenu.NewGame();
    }
    public void ExitGame()
    {
        pauseMenu.ExitGame();
    }
}
