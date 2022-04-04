using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ocnus : InteractableParent
{
    public RectTransform rope;
    public Vector3 ropeCutoffPoint;
    public int numberOfBites = 4;

    private Slider slide;
    private int currentBites = 0;
    private float nextValue;

    private void Start()
    {
        slide = GetComponent<Slider>();
        rope.anchoredPosition = ropeCutoffPoint;
        decaySpeed = initialSpeed;
        nextValue = slide.maxValue;
    }

    private void Update()
    {
        //Here the ending point is purposefully set at y=0, so I don't have to do any calculations
        //It's fine for a game jam
        if (!gameOver)
        {
            if (rope.anchoredPosition.y <= warningDistance)
            {
                warningFX.SetActive(true);
            }
            else
            {
                warningFX.SetActive(false);
            }

            rope.position -= Vector3.up * decaySpeed * Time.deltaTime;

            if (rope.anchoredPosition.y <= gameOverDistance)
            {
                slide.interactable = false;
                GameOver();
            }
        }
    }

    public void SliderValueChanged(float value)
    {
        if (value == slide.maxValue && nextValue == slide.maxValue)
        {
            nextValue = slide.minValue;
            currentBites++;
        }
        else if (value == slide.minValue && nextValue == slide.minValue)
        {
            nextValue = slide.maxValue;
            currentBites++;
        }

        if (currentBites == numberOfBites)
        {
            currentBites = 0;
            rope.anchoredPosition = ropeCutoffPoint;
        }
    }
}
