using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Rigidbody2D BulletRigidBody;
    public int BulletHealthChange;

    private float BulletTime;
    private float BulletDirection;
    public void Start()
    {
        //make sure the bullet is set to the right speed
        BulletHealthChange = PlayerVariableSet.BulletHealthChange;

        // indicate the direction the bullet should go in
        switch (PlayerVariableSet.FacingRight)
        {
            case 0:
                BulletDirection = -1;
                this.gameObject.transform.localScale = new Vector3(-0.3f, 0.3f, 0.3f);
                break;
            case 1:
                BulletDirection = 1;
                this.gameObject.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                break;
        }
    }
    public void Update()
    {
        BulletRigidBody.velocity = new Vector2(BulletDirection * PlayerVariableSet.BulletSpeed, BulletRigidBody.velocity.y);

        //bullet despawn timer
        BulletTime = BulletTime + Time.deltaTime;

        if (BulletTime > PlayerVariableSet.BulletDuration)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //collision sound effect
        if(collision.tag == "Ground" || collision.tag == "Enemy")
        {
            PlayerVariableSet.VS.PlayerAudioSource1.clip = PlayerVariableSet.VS.AudioClips[0];
            PlayerVariableSet.VS.PlayerAudioSource1.volume = 0.7f;
            PlayerVariableSet.VS.PlayerAudioSource1.Play();
            GameObject.Destroy (this.gameObject);
        }
    }
}
