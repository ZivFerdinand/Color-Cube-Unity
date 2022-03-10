using System.Collections;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    private Vector2 VectorDebugger = new Vector2();
    public float rollDuration = 1f;
    public static bool isRolling;
    public Transform pivot;
    public Transform ghostPlayer;
    public LayerMask contactWallLayer;
    public InputManager inputManager;
    [SerializeField] private float fallDuration = 0.8f;
    private bool hasFallen = false;


    private void Awake()
    {
        Initialize();
    }

    void Initialize()
    {
        VectorDebugger = new Vector2();
        inputManager.onSwipeDetected += Roll;
        isRolling = false;
    }

    public void FallingAnimation()
    {
        transform.LeanMoveY(1, fallDuration).setEaseInOutSine().setOnComplete(() => { hasFallen = true; });
    }

    private void Roll(InputManager.Direction direction)
    {
        if(!hasFallen)
            return;
        StartCoroutine(RollToDirection(direction));
    }

    private IEnumerator RollToDirection(InputManager.Direction swipeDirection)
    {

        if (Database.Functions.InRangeInclusive(0f, Database.LevelRelated.gridLevelSize - 1, VectorDebugger.x - Database.Functions.DirectionEnumToVectorUnity(swipeDirection).x) &&
        Database.Functions.InRangeInclusive(0f, Database.LevelRelated.gridLevelSize - 1, VectorDebugger.y - Database.Functions.DirectionEnumToVectorUnity(swipeDirection).y))
        {
            if (!isRolling)
            {
                isRolling = true;

                float angle = 90f;
                Vector3 axis = GetAxis(swipeDirection);
                Vector3 directionVector = GetDirectionVector(swipeDirection);
                Vector2 pivotOffset = GetPivotOffset(swipeDirection);

                pivot.position = transform.position + (directionVector * pivotOffset.x) + (Vector3.down * pivotOffset.y);

                //Simulate Before the Action In Order To Get Ideal Result
                CopyTransformData(transform, ghostPlayer);
                ghostPlayer.RotateAround(pivot.position, axis, angle);


                GameObject rotateParent = new GameObject("RotateParent");
                rotateParent.transform.position = pivot.transform.position;
                transform.SetParent(rotateParent.transform);

                rotateParent.LeanRotate(axis * 90, rollDuration).setEaseInOutQuad().setOnComplete(() =>
                {
                    transform.SetParent(null);
                    Destroy(rotateParent);
                    CopyTransformData(ghostPlayer, transform);
                    isRolling = false;
                    if (swipeDirection == InputManager.Direction.Right)
                        VectorDebugger -= Vector2.right;
                    if (swipeDirection == InputManager.Direction.Down)
                        VectorDebugger -= Vector2.down;
                    if (swipeDirection == InputManager.Direction.Up)
                        VectorDebugger -= Vector2.up;
                    if (swipeDirection == InputManager.Direction.Left)
                        VectorDebugger -= Vector2.left;
                });

                yield return null;
            }
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