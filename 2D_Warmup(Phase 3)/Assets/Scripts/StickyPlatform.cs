using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlatform : MonoBehaviour
{

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.transform.SetParent(transform);//when the player jump on the platform it is a child object in it
        }
    }

    private void OnTriggerExit2D (Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.transform.SetParent(null); //reset when out of platform
        }
    }

}
