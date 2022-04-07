using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Sisyphus : InteractableParent, IPointerDownHandler
{
    //Mechanic - click to slide him back a bit
    [Range(0f, 1f)]
    public float pushBackAmount;
    public Transform startPoint;
    public Transform endPoint;

    private float t = 0;

    private Vector3 startPos;
    private Vector3 endPos;

    private void Start()
    {
        rect = GetComponent<RectTransform>();

        startPos = startPoint.position;
        endPos = endPoint.position;

        rect.position = startPos;

        decaySpeed = initialSpeed;
    }

    private void Update()
    {
        MoveObject();
    }

    private void MoveObject()
    {
        if (!gameOver)
        {
            if (Vector3.Distance(rect.position, endPos) > gameOverDistance)
            {
                if (Vector3.Distance(rect.position, endPos) < warningDistance)
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

                rect.position = Vector3.Lerp(startPos, endPos, t);
                t += Time.deltaTime * decaySpeed;
            }
            else
            {
                GameOver();
            }
        }
    }

    private void PushBackwards()
    {
        t -= pushBackAmount;
        t = Mathf.Clamp(t, 0f, 1f);
        rect.position = Vector3.Lerp(startPos, endPos, t);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!gameOver)
        {
            PushBackwards();
        }

    }
}
