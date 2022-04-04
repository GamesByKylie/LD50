using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salmoneus : InteractableParent
{
    public float leftMaxAngle;
    public float rightMaxAngle;

    public float rotateAmount;

    private void Start()
    {
        decaySpeed = initialSpeed;
    }

    private void Update()
    {
        if (!gameOver)
        {
            int sign = 0;
            if (transform.rotation.z == 0)
            {
                sign = Random.Range(-1, 2);
            }
            else
            {
                //to the left - between 0 and positive
                if (LeftToCenter(leftMaxAngle))
                {
                    sign = 1;
                }
                //to the right - between 360 and positive
                else if (CenterToRight(rightMaxAngle))
                {
                    sign = -1;
                }
            }

            transform.eulerAngles += Vector3.forward * sign * decaySpeed * Time.deltaTime;

            if (sign == 1 && transform.eulerAngles.z > leftMaxAngle + gameOverDistance)
            {
                GameOver();
            }
            if (sign == -1 && transform.eulerAngles.z < rightMaxAngle + gameOverDistance)
            {
                GameOver();
            }
        }
    }

    public void Rotate(int sign)
    {
        transform.eulerAngles += Vector3.forward * sign * rotateAmount;
    }

    private bool LeftToCenter(float leftMax)
    {
        return transform.eulerAngles.z < leftMax && transform.rotation.z > 0;
    }

    private bool CenterToRight(float rightMax)
    {
        return transform.eulerAngles.z > rightMax && transform.rotation.z < 360;
    }
}
