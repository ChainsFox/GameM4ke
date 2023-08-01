using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    //this logic only work better on top down perspective, not 2d

    public GameObject player;
    public Animator anim;
    public Rigidbody2D rb;
    //public PlayerLife PlayerLifeLogic;
    private float distance;
    //private SpriteRenderer sprite;
    [SerializeField] public float speed = 2f;
    [SerializeField] public float distanceBetween = 8f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();  
        //sprite = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        //Debug.Log(distance);
        //Vector2 direction = player.transform.position - transform.position;

        //transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);

        if (distance < distanceBetween)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            anim.SetBool("Mushroom_Idle", false);
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            if (player.transform.position.x >= 0.01f)
            {
                //sprite.flipX = false;
                transform.localScale = new Vector3(-1f, 1f, 1f);
                anim.SetBool("Mushroom_Running", true);
            } 
            else if (player.transform.position.x <= - 0.01f)
            {
                //sprite.flipX = true;
                transform.localScale = new Vector3(1f, 1f, 1f);
                anim.SetBool("Mushroom_Running", true);
            }
            //else if (distance <= 2.0f)
            //{
            //    anim.SetBool("Mushroom_Idle", true);
            //}
        }
        else
        {
            //rb.bodyType = RigidbodyType2D.Static;
            transform.position = new Vector2(transform.position.x,transform.position.y);
            anim.SetBool("Mushroom_Running", false);
            anim.SetBool("Mushroom_Idle", true);
        }
        if (distance <= 1.2f)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y);
            anim.SetBool("Mushroom_Running", false);
            anim.SetBool("Mushroom_Idle", true);
        }


        //if (distance <= 1.2f)
        //{
        //    transform.position = new Vector2(transform.position.x,transform.position.y);
        //    anim.SetBool("Mushroom_Running", false);
        //    anim.SetBool("Mushroom_Idle", true);
        //}


    }


    public void OnTriggerEnter2D (Collider2D collision)
    {
        //if (distance <= 2.0f)
        //{
        //    transform.position = new Vector2(transform.position.x,transform.position.y);
        //}
        PlayerLife playerLife = collision.GetComponent<PlayerLife>();
        if (playerLife != null)
        {
            playerLife.Die();
        }

    }









}
