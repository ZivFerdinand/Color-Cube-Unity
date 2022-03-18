using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RotateObject : MonoBehaviour
{
    public float rotSpeed;
    void Update()
    {
        transform.Rotate(0, rotSpeed * Time.deltaTime, 0, Space.Self);
    }
}
