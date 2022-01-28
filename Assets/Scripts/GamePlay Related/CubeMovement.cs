using System.Collections;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    public float rollDuration = 1f;
    public static bool isRolling;
    public Transform pivot;
    public Transform ghostPlayer;
    public LayerMask contactWallLayer;

    public InputManager inputManager;

    private void Awake()
    {
        Initialize();
    }

    void Initialize()
    {
        inputManager.onSwipeDetected += Roll;
        isRolling = false;
    }

    private void Roll(InputManager.Direction direction)
    {
        StartCoroutine(RollToDirection(direction));
    }

    private IEnumerator RollToDirection(InputManager.Direction swipeDirection)
    {
        if (!isRolling)
        {
            isRolling = true;

            float angle = 90f;
            Vector3 axis = GetAxis(swipeDirection);
            Vector3 directionVector = GetDirectionVector(swipeDirection);
            Vector2 pivotOffset = GetPivotOffset(swipeDirection);

            pivot.position = transform.position + (directionVector * pivotOffset.x) + (Vector3.down * pivotOffset.y);

            //simulate before the action in order to get an ideal result
            CopyTransformData(transform, ghostPlayer);
            ghostPlayer.RotateAround(pivot.position, axis, angle);

            float elapsedTime = 0f;

            while (elapsedTime < rollDuration)
            {
                elapsedTime += Time.deltaTime;

                transform.RotateAround(pivot.position, axis, (angle * (Time.deltaTime / rollDuration)));
                yield return null;
            }

            CopyTransformData(ghostPlayer, transform);

            isRolling = false;
        }
        
    }

    public void CopyTransformData(Transform source, Transform target)
    {
        target.localPosition = source.localPosition;
        target.localEulerAngles = source.localEulerAngles;
    }

    private Vector3 GetAxis(InputManager.Direction direction)
    {
        switch (direction)
        {
            case InputManager.Direction.Left:
                return Vector3.forward;
            case InputManager.Direction.Up:
                return Vector3.right;
            case InputManager.Direction.Right:
                return Vector3.back;
            case InputManager.Direction.Down:
                return Vector3.left;
            default:
                return Vector3.zero;
        }
    }

    private Vector3 GetDirectionVector(InputManager.Direction direction)
    {
        switch (direction)
        {
            case InputManager.Direction.Left:
                return Vector3.left;
            case InputManager.Direction.Up:
                return Vector3.forward;
            case InputManager.Direction.Right:
                return Vector3.right;
            case InputManager.Direction.Down:
                return Vector3.back;
            default:
                return Vector3.zero;
        }
    }

    private Vector2 GetPivotOffset(InputManager.Direction direction)
    {
        Vector2 pivotOffset = Vector2.zero;
        Vector2 center = transform.GetComponent<BoxCollider>().size / 2f;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.up, out hit, 100f, contactWallLayer))
        {
            switch (hit.collider.name)
            {
                case "X":
                    if (direction == InputManager.Direction.Left || direction == InputManager.Direction.Right)
                        pivotOffset = new Vector2(center.y, center.x);
                    else
                        pivotOffset = Vector2.one * center.x;
                    break;
                case "Y":
                    pivotOffset = center;
                    break;
                case "Z":
                    if (direction == InputManager.Direction.Up || direction == InputManager.Direction.Down)
                        pivotOffset = new Vector2(center.y, center.x);
                    else
                        pivotOffset = Vector2.one * center.x;
                    break;
            }
        }
        return pivotOffset;
    }
}