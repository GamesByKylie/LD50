using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Snake : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public int ID;
    public float minRotation;
    public float maxRotation;
    [Tooltip("Only the sign matters here")]
    public int direction;

    private OtusAndEphialtes gameParent;
    private bool isHeld = false;
    private RectTransform rect;

    public void OnPointerDown(PointerEventData eventData)
    {
        isHeld = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHeld = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHeld = false;
    }

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        gameParent = GetComponentInParent<OtusAndEphialtes>();
        rect.rotation = Quaternion.Euler(0f, 0f, minRotation);

        direction = (int)Mathf.Sign(direction);
    }

    private void Update()
    {
        if (!gameParent.IsGameOver)
        {
            //transform.eulerAngles = new Vector3(0f, 0f, Mathf.Clamp(transform.eulerAngles.z, minRotation, maxRotation));

            if (!isHeld)
            {
                bool needsWarning = Mathf.Abs((maxRotation > 0 ? maxRotation : 360 + maxRotation) - transform.eulerAngles.z) <= gameParent.warningDistance;
                gameParent.ToggleWarning(needsWarning, ID);

                rect.rotation = Quaternion.Euler(0f, 0f, rect.eulerAngles.z + (direction * gameParent.CurrentSpeed * Time.deltaTime));
                //transform.eulerAngles += direction * Vector3.forward * gameParent.CurrentSpeed * Time.deltaTime;

                needsWarning = Mathf.Abs((maxRotation > 0 ? maxRotation : 360 + maxRotation) - transform.eulerAngles.z) <= gameParent.gameOverDistance;
                if (needsWarning)
                {
                    gameParent.GameOver();
                }
            }
            else
            {
                float previousAngle = transform.eulerAngles.z;

                Vector3 dir = -direction * (Input.mousePosition - transform.position);
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(-direction * angle, direction * Vector3.back);

                float z = transform.eulerAngles.z;

                if (Mathf.Abs(maxRotation - z) < Mathf.Abs(maxRotation - previousAngle))
                {
                    transform.eulerAngles = Vector3.forward * previousAngle;
                }
                else
                {
                    //You won't be dragging these forwards, so we only need to check for the minimum
                    //Also, we only have to calculate this if we know the player isn't trying to drag forwards
                    if (minRotation > 0)
                    {
                        if (maxRotation < 0)
                        {
                            float normalizedMax = maxRotation + 360f;
                            Debug.Log(z);
                            if (z < normalizedMax)
                            {
                                transform.eulerAngles = Vector3.forward * Mathf.Min(z, minRotation);
                            }
                            //I spent SO MANY HOURS trying to figure out why you can't drag past 0
                            //Never did figure out the reason, but turns out if you just skip from negative (or close to 360) to positive (or close to 0) it works??
                            //And it's such a small change it's unnoticeable
                            if (z > 350 && 360f - z < 2f)
                            {
                                transform.eulerAngles = Vector3.forward;
                            }
                        }
                        else
                        {
                            transform.eulerAngles = Vector3.forward * Mathf.Max(z, minRotation);
                        }
                    }
                    else
                    {
                        transform.eulerAngles = Vector3.forward * Mathf.Min(z, minRotation);
                    }
                }
            }
        }
    }
       
}
