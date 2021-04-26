using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehave : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] ParticleSystem speedParticle;
    [SerializeField] GameObject lavaGen, score, gameOverPanel, fadeToScene;
    public bool didEnd;
    float onWallTime;
    float onWallDuration = 1;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        //set particle speed according to player y acis velocity
        var speedParticleSetting = speedParticle.main;
        speedParticleSetting.startSpeed = Mathf.Clamp((-rb.velocity.y / 2), 0f, 30f)  + 5 ;


        if (onWallTime < onWallDuration)
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1 - (onWallTime / onWallDuration), 1 - (onWallTime / onWallDuration));
        }
        else
        {
            if (didEnd)
            {
                return;
            }
            StartCoroutine(EndGame());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            StartCoroutine(EndGame());
            
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            onWallTime += Time.deltaTime;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            onWallTime = 0;
        }
    }
    public IEnumerator EndGame()
    {
        didEnd = true;
        Debug.Log("DIE");
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        LeanTween.alphaCanvas(gameOverPanel.GetComponent<CanvasGroup>(), 1, .5f);
        fadeToScene.SetActive(true);

        yield return new WaitForSeconds(.5f);
        
        lavaGen.SetActive(false);
        LeanTween.moveX(gameObject, Camera.main.transform.position.x, 1f);
        LeanTween.scale(score, Vector2.one * 1.5f, .5f)
            .setEaseInOutSine();

    }
    private void OnParticleCollision(GameObject other)
    {
        if (didEnd)
        {
            return;
        }
        StartCoroutine(EndGame());
    }
}
