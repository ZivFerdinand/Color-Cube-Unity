using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeMovement : MonoBehaviour
{
    [SerializeField] private float _rollSpeed = 5;
    private bool _isMoving;
 
    private void Update() {
        if (_isMoving) return;
        if(Mathf.Abs(transform.position.y - 1.420776f) > 0.01f) return;

        if      (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))    Assemble(Vector3.left);
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))   Assemble(Vector3.right);
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))      Assemble(Vector3.forward);
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))    Assemble(Vector3.back);

        void Assemble(Vector3 dir) {
            var anchor = transform.position + (Vector3.down + dir) * 0.5f;
            var axis = Vector3.Cross(Vector3.up, dir);
            StartCoroutine(Roll(anchor, axis));
        }
    }
 
    private IEnumerator Roll(Vector3 anchor, Vector3 axis) {
        _isMoving = true;
        for (var i = 0; i < 90 / _rollSpeed; i++) {
            transform.RotateAround(anchor, axis, _rollSpeed);
            yield return new WaitForSeconds(0.01f);
        }
        _isMoving = false;
    }
}
