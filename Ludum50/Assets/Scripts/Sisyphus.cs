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
    public LineRenderer path;

    private float t = 0;
    private Vector3 startPos;
    private Vector3 endPos;

    private void Start()
    {
        rect = GetComponent<RectTransform>();

        startPos = path.GetPosition(0);
        endPos = path.GetPosition(path.positionCount - 1);

        rect.position = startPos;

        currentSpeed = initialSpeed;
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
                t += Time.deltaTime * currentSpeed;
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
        PushBackwards();
    }
}
