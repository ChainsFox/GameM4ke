using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagePopup : MonoBehaviour
{
    public GameObject Message;
    public Collider2D coll;

    private void Start ()
    {
        coll = gameObject.GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Audio_Manager.Instance.PlaySFX("SwiftSFX");
            coll.enabled = false;
            Message.SetActive(true);
        }
    }
    //private void OnTriggerExit2D (Collider2D other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        Message.SetActive(false);
    //    }
    //}

}
