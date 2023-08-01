using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlantWeapon : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject BulletPrefab;
    private GameObject Player;
    private Animator anim;
    [SerializeField] public float range = 13f;

    private float timer;
    //public float delay = 0.2f;
    //float seconds;



    void Start ()
    {

        anim = GetComponent<Animator>();
        transform.Rotate(0f,180f,0f);
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    
    void Update()
    {
        
        //Shoot in range
        float distance = Vector2.Distance(transform.position, Player.transform.position);
        //Debug.Log(distance);


        if (distance < range)
        {
            timer += Time.deltaTime;


            if (timer >= 2)
            {
                timer = 0;
                anim.SetBool("IsAttacking", true);
                //Invoke("Shoot", 0.4f); //this only work 1 time 
                //InvokeRepeating("Shoot", 0.4f, 100f); //you can but this just feels wrong
                InvokeRepeating(nameof(Shoot), 0.42f, 0f); //it work, but why tho?
                //Shoot();
            }


        }
        else
        {
            anim.SetBool("IsAttacking", false);
        }
        




        //Shoot automatically
        //timer += Time.deltaTime;

        //if (timer > 2)
        //{
        //    timer = 0;
        //    Shoot();
        //}


    }


    void Shoot()
    {
        Audio_Manager.Instance.PlaySFX("PlantShootSFX");
        Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation); //Quaternion.identity

    }


}
