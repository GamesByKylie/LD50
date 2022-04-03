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
            Vector3 dir;
            if (direction < 0)
            {
                dir = Input.mousePosition - transform.position;
            }
            else
            {
                dir = transform.position - Input.mousePosition;
            }

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if (direction < 0)
            {
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            else
            {
                transform.rotation = Quaternion.AngleAxis(-angle, Vector3.back);
            }
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
