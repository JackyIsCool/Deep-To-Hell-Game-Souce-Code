using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    Vector3 startPos;
    private void Start()
    {
        startPos = transform.position;
    }
    private void FixedUpdate()
    {
        transform.position = new Vector3(startPos.x, target.position.y, startPos.z);
    }
}
