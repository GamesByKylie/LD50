using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractableParent : MonoBehaviour
{
    public float initialSpeed;
    public float warningDistance = 10f;
    public float gameOverDistance = 0.1f;
    public GameObject warningFX;

    protected bool gameOver = false;
    protected RectTransform rect;

    protected Vector3 previousMousePos;
    
    protected void GameOver()
    {
        gameOver = true;
    }

}
