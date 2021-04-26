using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreCounter : MonoBehaviour
{

    private void Update()
    {
        if (FindObjectOfType<PlayerBehave>().didEnd)
        {
            return;
        }
        GetComponent<TextMeshProUGUI>().text = "Score: " + Mathf.FloorToInt(Time.timeSinceLevelLoad);
    }
}
