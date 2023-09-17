using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public GameObject player;
    public GameObject respawnPoint;
    public BoxCollider2D col;
    //public PlayerMovement PMlogic;
    public PlayerController PClogic;
    public Audio_Manager Audio;
    public bool IsDead;

    //public GameManagerScript gameManager;
    //public PlayerMovement Pm;


    //[SerializeField] private AudioSource deathSoundEffect;
    //[SerializeField] private AudioSource jumpSoundEffect;


    private void Start()
    {   
        //PMlogic = GetComponent<PlayerMovement>();
        PClogic = GetComponent<PlayerController>();
        col = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        

        
        //FindObjectOfType<AudioManager>().Enable("JumpSFX"); //!!! it will always play when you reload a scene   
         
        
    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap") /*&& !isDead*/)
        {
            //isDead = true; //so that it only happen once
            //gameManager.gameOver();
            col.enabled = false;
            IsDead = true;
            if (IsDead == true)
            {
                anim.SetBool("IsWallSliding", false);
                Die(); 
            }
            else
            {
                CancelInvoke(nameof(Die));
            }
            
            //if (PMlogic.IsGrounded())
            //{
            //    Audio_Manager.Instance.DisableSFX("JumpSFX");
            //}
            if (PClogic.IsGrounded())
            {
                Audio_Manager.Instance.DisableSFX("JumpSFX");
            }
            
            
        }   
    }

           

    public void Die()
    {
        //deathSoundEffect.Play();
        //FindObjectOfType<AudioManager>().Play("DeathSFX");
        Audio_Manager.Instance.PlaySFX("DeathSFX");
        //FindObjectOfType<AudioManager>().Disable("JumpSFX");
        //jumpSoundEffect.enabled = false; //fix the bug when you die on the ground you can still hear the jumpsf if u press space (old)  
        rb.Sleep();
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death"); 
        //Invoke("RestartLevel", 1.5f);
        Invoke(nameof(RespawnLogic), 1.5f);

    }

    //private void RestartLevel () // old method of using it in the animation event
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().name); //name is the name of the active scence method. This is updated by the GameManager Script
    //}

    private void RespawnLogic ()
    {
        rb.WakeUp();
        //Audio_Manager.Instance.EnableSFX("JumpSFX");
        col.enabled = true;
        IsDead = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        anim.SetTrigger("RespawnAlive");
        player.transform.position = respawnPoint.transform.position;
        Audio_Manager.Instance.PlaySFX("RespawnSFX");

    }

    public void UpdateCheckPoint(GameObject pos) //update the checkpoint by updating the gameobject 
    {
        respawnPoint = pos;
    }
 
}
