using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractableParent : MonoBehaviour
{
    public float initialSpeed;
    public float speedIncrease = 0.1f;
    public float warningDistance = 10f;
    public float gameOverDistance = 0.1f;
    public GameObject warningFX;
    public GameObject gameOverFX;

    [SerializeField] protected GameController gc = null;
    protected bool gameOver = false;
    protected RectTransform rect;
    protected float decaySpeed;
    
    public void GameOver()
    {
        gameOver = true;
        gameOverFX.SetActive(true);
        gc.CheckGameOver();
    }

    public bool IsGameOver
    {
        get
        {
            return gameOver;
        }
    }

    public void IncreaseDecaySpeed()
    {
        decaySpeed += speedIncrease;
    }
}
