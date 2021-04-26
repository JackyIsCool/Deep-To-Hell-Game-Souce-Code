using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NavigatorBar : MonoBehaviour
{
    [SerializeField] Transform startPoint, endPoint, player, slider;
    private void Update()
    {
        //get how many distance has pass
        float distance = startPoint.position.y - endPoint.position.y;
        float percentage = -player.position.y / distance;

        float barLength = GetComponent<RectTransform>().sizeDelta.y;
        slider.GetComponent<RectTransform>().anchoredPosition = Vector2.down * barLength * percentage;
    }
}
