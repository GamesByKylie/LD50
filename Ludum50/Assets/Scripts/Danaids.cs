using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Danaids : InteractableParent, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public Slider slide;
    public GameObject waterPour;
    public float drainSpeed;

    private bool isHeld = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        waterPour.SetActive(true);
        isHeld = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        waterPour.SetActive(false);
        isHeld = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        waterPour.SetActive(false);
        isHeld = false;
    }

    void Start()
    {
        currentSpeed = initialSpeed;
        slide.value = slide.minValue;
        waterPour.SetActive(false);
    }

    private void Update()
    {
        if (!gameOver)
        {
            if (!isHeld)
            {
                slide.value += Time.deltaTime * currentSpeed;

                if (slide.value >= slide.maxValue - warningDistance)
                {
                    warningFX.SetActive(true);
                }
                else
                {
                    warningFX.SetActive(false);
                }

                if (slide.value >= slide.maxValue)
                {
                    GameOver();
                }
            }
            else
            {
                slide.value -= drainSpeed * Time.deltaTime;
            }
        }
    }
}
