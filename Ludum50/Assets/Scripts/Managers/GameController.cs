using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Text timerText;
    public InteractableParent[] allMiniGames;
    
    public float difficultyIncreaseTimestep = 1f;
    public float difficultyIncreaseDelay = 10f;

    private float timer = 0;

    private void Start()
    {
        InvokeRepeating("IncreaseAllDifficulty", difficultyIncreaseDelay, difficultyIncreaseTimestep);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        UpdateTimerText();

    }

    public void CheckGameOver()
    {
        foreach (InteractableParent game in allMiniGames)
        {
            if (!game.IsGameOver)
            {
                return;
            }
        }

        PlayerPrefs.SetFloat("final_time", timer);
        Invoke("LoadGameOverScene", 0.1f);
    }

    private void LoadGameOverScene()
    {
        SceneManager.LoadScene(2);
    }

    private void IncreaseAllDifficulty()
    {
        foreach (InteractableParent game in allMiniGames)
        {
            game.IncreaseDecaySpeed();
        }
    }

    private void UpdateTimerText()
    {
        timerText.text = TimeText(timer);
    }

    public static string TimeText(float time)
    {
        int seconds = Mathf.FloorToInt(time);
        int minutes = 0;
        if (seconds >= 60)
        {
            minutes = Mathf.FloorToInt(seconds / 60f);
            seconds = seconds % 60;
        }

        string secondsText = seconds < 10 ? "0" + seconds : "" + seconds;
        string minutesText = minutes < 10 ? "0" + minutes : "" + minutes;

        return minutesText + ":" + secondsText;
    }
}
