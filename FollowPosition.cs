using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPosition : MonoBehaviour
{
    [SerializeField] bool shouldOffset;
    [HideInInspector] public Vector2 offset;
    private void Start()
    {
        if (shouldOffset)
        {
            offset = transform.position;
        }
    }
    private void FixedUpdate()
    {
        Vector2 playerPos = FindObjectOfType<PlayerBehave>().transform.position;
        transform.position = new Vector2(0, playerPos.y) + offset;
        //Debug.Log(offset);
    }
}
