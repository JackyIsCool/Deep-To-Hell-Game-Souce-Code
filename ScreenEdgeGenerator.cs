using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenEdgeGenerator : MonoBehaviour
{
    [SerializeField] GameObject wall;
    int screenX, screenY;

    void Start()
    {
        BuildWall();
        screenX = Screen.width;
        screenY = Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        if (screenX != Screen.width || screenY != Screen.height)
        {
            ClearWall();
            BuildWall();
        }
    }
    void BuildWall()
    {
        //left
        Instantiate(wall, Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height / 2, 10)), Quaternion.Euler(Vector2.zero)).transform.SetParent(transform);
        //right
        Instantiate(wall, Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / 2, 10)), Quaternion.Euler(Vector2.zero)).transform.SetParent(transform);

    }
    void ClearWall()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
