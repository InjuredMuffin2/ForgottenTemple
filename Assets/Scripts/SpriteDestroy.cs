using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDestroy : MonoBehaviour
{
    public GameObject ThisObject;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GameObject.Destroy(ThisObject);
        }
    }
}
