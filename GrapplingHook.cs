using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GrapplingHook : MonoBehaviour
{

    SpringJoint2D sj;
    LineRenderer predictorLR;
    Vector2 mousePosition;
    Vector2 gunPosition;

    [SerializeField] GameObject grapplingBullet, tutorial;
    public List<GrapplingLine> grapplingLines;


    private void Awake()
    {
        Time.timeScale = 0;
        LeanTween.scale(tutorial, Vector2.zero, 2f).setDestroyOnComplete(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        predictorLR = transform.GetChild(0).GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        RotateToCursor();

        if (Input.GetMouseButtonDown(0))
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
            if (FindObjectOfType<BulletRest>().currentBullet <= 0)
            {
                return;
            }
            //Create new bullet into list 
            GameObject bullet = Instantiate(grapplingBullet, transform.position, transform.rotation * Quaternion.Euler(Vector3.back * 90));
            GrapplingLine grapplingLine = new GrapplingLine
            {
                bullet = bullet,
                line = bullet.GetComponent<LineRenderer>(),
                springJoint = FindObjectOfType<PlayerBehave>().gameObject.AddComponent<SpringJoint2D>()
            };
            grapplingLine.springJoint.enabled = false;

            grapplingLines.Add(grapplingLine);
        }
        foreach (var item in grapplingLines)
        {
            float airTime = item.bullet.GetComponent<GrapplingBullet>().inAirTime;
            if (airTime >= 1)
            {
                BreakConnection(item);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            foreach (var item in grapplingLines.ToArray())
            {
                BreakConnection(item);
            }
        }
    }


    void RotateToCursor()
    {
        gunPosition = transform.position;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        float angle = Mathf.Atan2(mousePosition.y - gunPosition.y, mousePosition.x - gunPosition.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);

        //pointer
        predictorLR.SetPosition(0, gunPosition);
        predictorLR.SetPosition(1, mousePosition);
    }
    public void ConnectToBullet(Vector2 connectPoint, Transform transform)
    {
        foreach (var item in grapplingLines)
        {
            if (item.bullet.transform == transform)
            {
                sj = item.springJoint;
            }
        }
        sj.enabled = true;

        sj.enableCollision = true;
        sj.autoConfigureDistance = false;
        sj.frequency = 1f;
        sj.dampingRatio = 5;
        sj.distance = 2;
        sj.connectedAnchor = connectPoint;
    }
    public void BreakConnection(GrapplingLine grapplingLine)
    {
        grapplingLine.springJoint.frequency = 0;
        grapplingLine.springJoint.breakForce = 0;
        Destroy(grapplingLine.springJoint);

        grapplingLines.Remove(grapplingLine);
        Destroy(grapplingLine.bullet);
    }
    
}
[System.Serializable]
public class GrapplingLine
{
    public GameObject bullet;
    public LineRenderer line;
    public SpringJoint2D springJoint;
}