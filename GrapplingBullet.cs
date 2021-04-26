using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingBullet : MonoBehaviour
{
    [SerializeField] float speed;
    bool isSticked;
    [HideInInspector] public float inAirTime;
    GrapplingHook grapplingHook;
    LineRenderer lr;
    EdgeCollider2D ec;
    private void Start()
    {
        lr = GetComponent<LineRenderer>();
        ec = GetComponent<EdgeCollider2D>();
        grapplingHook = FindObjectOfType<GrapplingHook>();
    }
    private void Update()
    {
        lr.SetPosition(0, grapplingHook.transform.position);
        lr.SetPosition(1, transform.position);

        List<Vector2> points = new List<Vector2>
        {
            transform.InverseTransformPoint(grapplingHook.transform.position),
            transform.InverseTransformPoint(transform.position)
        };
        ec.points = points.ToArray();

    }
    void FixedUpdate()
    {
        
        if (!isSticked)
        {
            inAirTime += Time.fixedDeltaTime;
            transform.position += transform.up * speed * Time.fixedDeltaTime;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        grapplingHook.ConnectToBullet(transform.position, transform);
        isSticked = true;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
    }
    private void OnParticleCollision(GameObject other)
    {
        foreach (var item in grapplingHook.grapplingLines.ToArray())
        {
            if (item.bullet.transform == transform)
            {
                grapplingHook.BreakConnection(item);
                FindObjectOfType<BulletRest>().currentBullet -= 1;
            }
        }
        
    }

}
