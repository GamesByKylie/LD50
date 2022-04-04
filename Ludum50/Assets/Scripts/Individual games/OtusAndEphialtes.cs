using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtusAndEphialtes : InteractableParent
{
    private bool snake1Warning = false;
    private bool snake2Warning = false;

    private void Start()
    {
        decaySpeed = initialSpeed;
    }

    public void ToggleWarning(bool toggle, int ID)
    {
        if (ID == 1)
        {
            snake2Warning = toggle;
        }
        else if (ID == 0)
        {
            snake1Warning = toggle;
        }

        warningFX.SetActive(snake2Warning || snake1Warning);
    }

    public float CurrentSpeed
    {
        get
        {
            return decaySpeed;
        }
    }

    public bool IsGameOver
    {
        get
        {
            return gameOver;
        }
    }
}
