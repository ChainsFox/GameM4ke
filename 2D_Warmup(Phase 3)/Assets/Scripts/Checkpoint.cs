using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Respawn respawn;
    private BoxCollider2D checkPointCollider;
    private PlayerLife PL;
    SpriteRenderer spriteRenderer;
    public Sprite passive, active;
    

    void Awake() //awake happen before start
    {
        checkPointCollider = GetComponent<BoxCollider2D>();
        respawn = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Respawn>();
        PL = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLife>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }


    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //PL.respawnPoint = this.gameObject;
            PL.UpdateCheckPoint(this.gameObject); //communicate with the PlayerLife script that this is the new Gameobject(aka checkpoint)
            respawn.respawnPoint = this.gameObject; //communicate with the respawn script that this is the new checkpoint
            checkPointCollider.enabled = false; //when the player walk into this collider it will set the new respawn point and disable this collider.
            Audio_Manager.Instance.PlaySFX("CheckpointSFX");
            spriteRenderer.sprite = active;
            
        }
    }
}
