using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameManager : MonoBehaviour
{
    public Text currentScoreText;
    public Text highScoreText;
    public Text newHighScoreText;

    private void Start()
    {
        currentScoreText.text = "Your Score: " + GameController.TimeText(PlayerPrefs.GetFloat("final_time"));

        if (PlayerPrefs.GetFloat("final_time") > PlayerPrefs.GetFloat("high_score"))
        {
            newHighScoreText.gameObject.SetActive(true);
            PlayerPrefs.SetFloat("high_score", PlayerPrefs.GetFloat("final_time"));
        }
        else
        {
            newHighScoreText.gameObject.SetActive(false);
        }

        highScoreText.text = "High Score: " + GameController.TimeText(PlayerPrefs.GetFloat("high_score"));
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
