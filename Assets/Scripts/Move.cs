using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    [SerializeField]

    public float moveSpeed = 5f;
    public float previousDistanceToTouchPos;
    public float currentDistanceToTouchPos;

    bool isMoving = false;

    Rigidbody2D rb;

    Touch touch;
    Vector3 touchPosition;
    Vector3 whereToMove;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

  
    void Update()
    {
        if (isMoving)
        {
            currentDistanceToTouchPos = (touchPosition - transform.position).magnitude;
        }

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            previousDistanceToTouchPos = 0;
            currentDistanceToTouchPos = 0;
            isMoving = true;
            touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            touchPosition.z = 0;
            whereToMove = (touchPosition - transform.position).normalized;
            rb.velocity = new Vector2(whereToMove.x * moveSpeed, whereToMove.y * moveSpeed);
        }
#else
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                previousDistanceToTouchPos = 0;
                currentDistanceToTouchPos = 0;
                isMoving = true;
                touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                touchPosition.z = 0;
                whereToMove = (touchPosition - transform.position).normalized;
                rb.velocity = new Vector2(whereToMove.x * moveSpeed, whereToMove.y * moveSpeed);
            }
        }
#endif

        if(currentDistanceToTouchPos > previousDistanceToTouchPos)
        {
            isMoving = false;
            rb.velocity = Vector2.zero;
        }

        if (isMoving)
        {
            previousDistanceToTouchPos = (touchPosition - transform.position).magnitude;
        }

    }
}
