using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    //int cherriesTotal = GameObject.FindGameObjectsWithTag("Cherry").Length; //to find total
    private int cherries = 0;

    [SerializeField] private Text cherriesText;
    //[SerializeField] private AudioSource collectionSoundEffect;

    private void OnTriggerEnter2D (Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Cherry"))
        {
            
            //collectionSoundEffect.Play();
            //FindObjectOfType<AudioManager>().Play("CollectSFX");
            Audio_Manager.Instance.PlaySFX("CollectSFX");
            Destroy(collision.gameObject);
            cherries++;
            cherriesText.text = "Cherries: " + cherries;
        }

    }



}
