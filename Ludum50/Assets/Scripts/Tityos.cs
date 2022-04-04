using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Tityos : InteractableParent, IPointerClickHandler
{
    public RectTransform wound;
    public float woundStartSize;

    public Sprite birdStanding;
    public Sprite birdEating;
    public float spriteChangeTime;

    public float expandPerClick;

    private Image img;
    private float clickTimer;

    public void OnPointerClick(PointerEventData eventData)
    {
        clickTimer = 0;
        img.sprite = birdEating;
        wound.localScale += Vector3.one * expandPerClick;
        if (wound.localScale.x > woundStartSize)
        {
            wound.localScale = Vector3.one * woundStartSize;
        }
    }

    private void Start()
    {
        currentSpeed = initialSpeed;
        img = GetComponent<Image>();
        img.sprite = birdStanding;
        wound.localScale = Vector2.one * woundStartSize;
    }

    private void Update()
    {
        if (!gameOver)
        {
            if (wound.localScale.x <= warningDistance)
            {
                warningFX.SetActive(true);
            }
            else
            {
                warningFX.SetActive(false);
            }

            if (img.sprite.Equals(birdEating))
            {
                if (clickTimer >= spriteChangeTime)
                {
                    img.sprite = birdStanding;
                }
                else
                {
                    clickTimer += Time.deltaTime;
                }
            }

            wound.localScale -= Vector3.one * currentSpeed * Time.deltaTime;

            if (wound.localScale.x <= gameOverDistance)
            {
                GameOver();
            }
        }
    }
}
