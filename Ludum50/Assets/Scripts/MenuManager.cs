using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    //There's no need for additive loading, so we can just go
    //Also, because this is a small project unlikely to be expanded, we can hard-code the indices
    //Main menu is 0, main gameplay is 1, game over is 2, and that's it

    public void MainGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void GameOverScene()
    {
        SceneManager.LoadScene(2);
    }

    public void StartMenuScene()
    {
        SceneManager.LoadScene(0);
    }
}
