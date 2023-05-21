using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public Rigidbody2D ThisPlatformRB;
    public int DirectionLeft;
    public float PlatformSpeed;
    private void Start()
    {
        PlatformSpeed = UnityEngine.Random.Range(1.5f, 4.0f);
    }

    private void Update()
    {
        switch (DirectionLeft)
        {
            case 0:
                ThisPlatformRB.velocity = new Vector2(1 * PlatformSpeed, ThisPlatformRB.velocity.y);
                break;
            case 1:
                ThisPlatformRB.velocity = new Vector2(-1 * PlatformSpeed, ThisPlatformRB.velocity.y);
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Point B")
        {
            DirectionLeft = 1;
        }
        else if (collision.name == "Point A")
        {
            DirectionLeft = 0;
        }
    }
}
