using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tantalus : InteractableParent
{
    private Slider slide;

    private float previousValue;

    private void Start()
    {
        slide = GetComponent<Slider>();
        slide.value = slide.minValue;
        decaySpeed = initialSpeed;
    }

    private void Update()
    {
        if (!gameOver)
        {
            slide.SetValueWithoutNotify(slide.value + Time.deltaTime * decaySpeed);
            previousValue = slide.value;

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
                slide.interactable = false;
                GameOver();
            }
        }
    }

    public void OnSliderValueChanged(float value)
    {
        if (value > previousValue)
        {
            //Debug.Log($"Previous value {previousValue} | Attempted new value {value}");
            slide.value = previousValue;
        }
    }
}
