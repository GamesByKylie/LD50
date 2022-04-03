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
            if (isHeld)
            {
                Vector3 dir = -direction * (Input.mousePosition - transform.position);

                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

                transform.rotation = Quaternion.AngleAxis(-direction * angle, direction * Vector3.back);
                float z = transform.eulerAngles.z;

                //You won't be dragging these forwards, so we only need to check for the minimum
                if (minRotation > 0)
                {
                    if (maxRotation < 0)
                    {
                        Debug.Log($"{name} max rotation negative ({maxRotation}) | current rotation {z}");
                        float normalizedMax = maxRotation + 360f;
                        if (!(z > normalizedMax && z < 360f))
                        {
                            transform.eulerAngles = Vector3.forward * Mathf.Min(z, minRotation);
                        }
                    }
                    else
                    {
                        Debug.Log($"{name} max rotation positive ({maxRotation}) | current rotation {z}");
                        transform.eulerAngles = Vector3.forward * Mathf.Max(z, minRotation);
                    }
                }
                else
                {
                    transform.eulerAngles = Vector3.forward * Mathf.Max(z, minRotation);
                }
            }
            

            //transform.eulerAngles = new Vector3(0f, 0f, Mathf.Clamp(transform.eulerAngles.z, minRotation, maxRotation));

            //if (!isHeld)
            //{
            //    gameParent.ToggleWarning(Mathf.Abs(rect.eulerAngles.z - maxRotation) <= gameParent.warningDistance, ID);

            //    rect.rotation = Quaternion.Euler(0f, 0f, rect.eulerAngles.z + (direction * gameParent.CurrentSpeed * Time.deltaTime));
            //    //transform.eulerAngles += direction * Vector3.forward * gameParent.CurrentSpeed * Time.deltaTime;

            //    if (Mathf.Abs(rect.eulerAngles.z - maxRotation) <= gameParent.gameOverDistance)
            //    {
            //        gameParent.GameOver();
            //    }
            //}
            //else
            //{
            //    transform.LookAt(Input.mousePosition);
            //}
        }
    }

    
}
