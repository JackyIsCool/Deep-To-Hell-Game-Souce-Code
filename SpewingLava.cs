using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpewingLava : MonoBehaviour
{
    float worldX, worldY;
    [SerializeField] GameObject lavaWarning, lava;
    Transform lavaCanvas;
    float gap;
    private void Start()
    {
        worldX = Camera.main.ScreenToWorldPoint(Vector3.right * Screen.width).x;
        worldY = Camera.main.ScreenToWorldPoint(Vector3.up * Screen.height).y;
        gap = worldX / 3;
        //make the lava size match the scene's 1/3
        RectTransform warningRect = lavaWarning.GetComponent<RectTransform>();
        warningRect.sizeDelta = new Vector2(800 / 3, warningRect.sizeDelta.y);

        lavaCanvas = lavaWarning.transform.parent;

        StartCoroutine(SpewLava());
        StartCoroutine(SpewLava());
    }
    IEnumerator SpewLava()
    {
        int generatePosIndex = Random.Range(-1, 2);
        Vector2 generatePos = Vector2.right * gap * generatePosIndex * 2;

        GameObject lavaWarn = Instantiate(lavaWarning, lavaCanvas);
        lavaWarn.GetComponent<FollowPosition>().offset = generatePos;

        //lava will pop warning
        LeanTween.alphaCanvas(lavaWarn.GetComponent<CanvasGroup>(), .3f, .5f)
            .setLoopPingPong(3);

        yield return new WaitForSeconds(3f);
        Vector2 playerPos = FindObjectOfType<PlayerBehave>().transform.position;
        //pop lava particle
        Instantiate(lava, generatePos + (Vector2)lava.transform.position/* + (Vector2)lava.transform.position + Vector2.up * playerPos.y*/, Quaternion.identity);


        StartCoroutine(SpewLava());
        Destroy(lavaWarn);

        
    }
}
