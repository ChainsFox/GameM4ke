using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineDown : MonoBehaviour
{
    public Animator anim;
    [SerializeField] float Bounce = 19f;

    private void Start ()
    {
        anim = GetComponent<Animator>();    
    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.down * Bounce, ForceMode2D.Impulse);

            //anim.Play("Trampoline_Bounce");
            anim.SetTrigger("JumpPad");
            Audio_Manager.Instance.PlaySFX("TrampolineBounceSFX");

        }
    }
}
