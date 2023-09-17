using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
        anim.SetBool("Mushroom_Running", true);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currentPoint.position -  transform.position; //give our enemy a direction to go towards. Which is gonna be a direction in the currentpoint

        if (currentPoint == pointB.transform)
        {
            rb.velocity = new Vector2(speed, 0); //point B need to be on the right
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0); //to go in the other direction/point A need to be on the left
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.05f && currentPoint == pointB.transform) //check if the enemy has reach the current point and if the current point is B
        {
            flip();
            currentPoint = pointA.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.05f && currentPoint == pointA.transform) //check if the enemy has reach the current point and if the current point is B
        {
            flip();
            currentPoint = pointB.transform;
        }


    }

    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1; //1*-1 = 1, -1*-1 = 1, to flip the sprite/scale;
        transform.localScale = localScale;
    }

    private void OnDrawGizmos ()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f); //the enemy will change when it hit the outside radius.  
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f); 
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }

}
