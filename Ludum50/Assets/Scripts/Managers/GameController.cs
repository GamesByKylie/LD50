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
        SceneManager.LoadScene(2);
    }

    private void IncreaseAllDifficulty()
    {
        Debug.Log("Increasing difficulty!");
        foreach (InteractableParent game in allMiniGames)
        {
            game.IncreaseDecaySpeed();
        }
    }

    private void UpdateTimerText()
    {
        int seconds = Mathf.FloorToInt(timer);
        int minutes = 0;
        if (seconds >= 60)
        {
            minutes = Mathf.FloorToInt(seconds / 60f);
            seconds = seconds % 60;
        }

        string secondsText = seconds < 10 ? "0" + seconds : "" + seconds;
        string minutesText = minutes < 10 ? "0" + minutes : "" + minutes;

        timerText.text = minutesText + ":" + secondsText;
    }
}
