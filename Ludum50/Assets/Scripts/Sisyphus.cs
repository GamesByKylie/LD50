using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Sisyphus : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public Transform startPos;
    public Transform endPos;
    public float initialSpeed;
    public float playerDragSpeed;
    public float warningDistance = 10f;
    public float loseDistance = 0.1f;
    public GameObject warningFX;

    private float t = 0;
    private bool gameOver = false;
    private bool isHeld = false;

    private void Start()
    {
        transform.position = startPos.position;
    }

    private void Update()
    {
        if (!gameOver && !isHeld)
        {
            if (DistanceToEnd() > loseDistance)
            {
                if (DistanceToEnd() < warningDistance)
                {
                    warningFX.SetActive(true);
                }
                else
                {
                    if (warningFX.activeSelf)
                    {
                        warningFX.SetActive(false);
                    }
                }

                transform.position = Vector3.Lerp(startPos.position, endPos.position, t);
                t += Time.deltaTime * initialSpeed;
            }
            else
            {
                GameOver();
            }
        }
        else if (!gameOver && isHeld)
        {
            transform.position = Vector3.Lerp(startPos.position, endPos.position, t);
            t -= Time.deltaTime * playerDragSpeed;
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("being held");
        isHeld = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("released");
        isHeld = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("pointer left");
        isHeld = false;
    }

    private float DistanceToEnd()
    {
        return Vector3.Distance(transform.position, endPos.position);
    }

    private void GameOver()
    {
        gameOver = true;
        Debug.Log("Sisyphus has escaped!");
    }

}
