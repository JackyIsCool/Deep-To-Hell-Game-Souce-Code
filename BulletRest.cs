using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRest : MonoBehaviour
{
    public int currentBullet = 3;

    private void Update()
    {
        for (int i = currentBullet; i <= 3; i++)
        {
            if (i == 3)
            {
                break;
            }
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
