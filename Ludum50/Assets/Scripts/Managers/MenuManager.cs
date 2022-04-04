using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Text highScoreText;

    private void Start()
    {
        if (PlayerPrefs.HasKey("high_score"))
        {
            highScoreText.text = "High Score: " + GameController.TimeText(PlayerPrefs.GetFloat("high_score"));
        }
        else
        {
            highScoreText.text = "High Score: 00:00";
            PlayerPrefs.SetFloat("high_score", 0f);
        }
    }

    //There's no need for additive loading, so we can just go
    //Also, because this is a small project unlikely to be expanded, we can hard-code the indices
    //Main menu is 0, main gameplay is 1, game over is 2, and that's it

    public void MainGameScene()
    {
        SceneManager.LoadScene(1);
    }

}
