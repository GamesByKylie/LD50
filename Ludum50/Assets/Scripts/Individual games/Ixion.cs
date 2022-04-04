using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ixion : InteractableParent, IPointerDownHandler, IPointerUpHandler
{
    public float maxRotSpeed;

    private float currentRotSpeed;
    private bool isHeld;
    private Vector3 prevMousePos;

    public void OnPointerDown(PointerEventData eventData)
    {
        prevMousePos = Input.mousePosition;
        isHeld = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHeld = false;
    }

    private void Start()
    {
        decaySpeed = initialSpeed;
        currentRotSpeed = maxRotSpeed;
    }

    private void Update()
    {
        if (!gameOver)
        {
            if (isHeld && (Input.mousePosition.x - prevMousePos.x) > 0)
            {
                Debug.Log("Being dragged to the left");
                currentRotSpeed = maxRotSpeed;
            }

            if (currentRotSpeed >= warningDistance)
            {
                warningFX.SetActive(true);
            }
            else
            {
                warningFX.SetActive(false);
            }

            transform.Rotate(0f, 0f, currentRotSpeed);

            currentRotSpeed += decaySpeed * Time.deltaTime;
            
            if (currentRotSpeed >= 0)
            {
                GameOver();
            }
        }
    }
}
