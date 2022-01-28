using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    public enum Direction { Left, Up, Right, Down, None }

    Direction direction;
    Vector2 startPos, endPos;
    public float swipeThreshold = 100f;
    bool draggingStarted;

    public Action<Direction> onSwipeDetected;
    private void Awake()
    {
        draggingStarted = false;
        direction = Direction.None;
    }
    private void Update()
    {
        //Debugger();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        draggingStarted = true;
        startPos = eventData.pressPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (draggingStarted)
        {
            endPos = eventData.position;

            Vector2 difference = endPos - startPos; // difference vector between start and end positions.

            if (difference.magnitude > swipeThreshold)
            {
                if (Mathf.Abs(difference.x) > Mathf.Abs(difference.y)) // Do horizontal swipe
                {
                    direction = difference.x > 0 ? Direction.Right : Direction.Left; // If greater than zero, then swipe to right.
                }
                else //Do vertical swipe
                {
                    direction = difference.y > 0 ? Direction.Up : Direction.Down; // If greater than zero, then swipe to up.
                }
            }
            else
            {
                direction = Direction.None;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (draggingStarted && direction != Direction.None)
        {
        //A swipe is detected
        if (onSwipeDetected != null)
            onSwipeDetected.Invoke(direction);
        }

        //reset the variables
        startPos = Vector2.zero;
        endPos = Vector2.zero;
        draggingStarted = false;
    }
    
    public void Debugger()
    {
        Debug.Log(direction);
    }
}