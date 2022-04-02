using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UIGlow : MonoBehaviour
{
    [Range(0f, 1f)]
    public float minAlpha;
    [Range(0f, 1f)]
    public float maxAlpha;

    public float glowSpeed;

    private Image img;
    private bool glow = false;
    private float currentAlpha;

    private float amplitude;
    private float verticalShift;

    private void Awake()
    {
        img = GetComponent<Image>();
        verticalShift = (maxAlpha + minAlpha) / 2.0f;
        amplitude = maxAlpha - verticalShift;

        currentAlpha = amplitude * Mathf.Sin(glowSpeed * Time.time) + verticalShift;
        SetAlphaColor(currentAlpha);

        Debug.Log($"Shifted {verticalShift} up, amplitude {amplitude}");
    }

    private void OnEnable()
    {
        glow = true;
    }

    private void OnDisable()
    {
        glow = false;
    }

    private void Update()
    {
        if (glow)
        {
            currentAlpha = amplitude * Mathf.Sin(glowSpeed * Time.time) + verticalShift;
            SetAlphaColor(currentAlpha);
        }
    }

    private void SetAlphaColor(float a)
    {
        img.color = new Color(img.color.r, img.color.g, img.color.b, a);
    }
}
