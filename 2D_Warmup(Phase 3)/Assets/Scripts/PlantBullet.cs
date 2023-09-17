using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlantBullet : MonoBehaviour
{
    [SerializeField]public float speed = 10f;
    public Rigidbody2D rb;
    public GameObject ImpactFX; 
    public PlayerLife PlayerLifeLogic;

    private float timer;

    void Start ()
    {
        rb.velocity = transform.right * speed;      

    }


    void Update()
    {
        //after a certain amount of time, the bullet will destroy itself if it does not hit anything.   
        timer = Time.deltaTime;
        if (timer > 10)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D (Collider2D HitInfo)
    {

        //Instantiate(ImpactFX, transform.position, transform.rotation); //this is for impact animation but i found it a bit off

        PlayerLife playerLife = HitInfo.GetComponent<PlayerLife>();
        //BUG!!!: if you die and second later the bullet hit you, it will repeat the death animation and cause it to fail.
        if (playerLife != null && PlayerLifeLogic.IsDead == false)
        { 
            playerLife.Die();
        }
        else
        {
            CancelInvoke(nameof(playerLife.Die));
        }

        //Debug.Log(HitInfo.name);
        //Audio_Manager.Instance.PlaySFX("CollectSFX");
        Destroy(gameObject);

        //playerLife != null && 

    }
}
