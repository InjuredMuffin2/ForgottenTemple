using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    public Animator PlayerAnimator;
    private void Start()
    {
        PlayerAnimator = GetComponent<Animator>();
    }
    public void Update()
    {
        //Set animator Variable for player being dead or alive
        if(PlayerVariableSet.IsAlive == true)
        {
            PlayerAnimator.SetBool("PlayerIsDead", false);
        }
        else
        {
            PlayerAnimator.SetBool("PlayerIsDead", true);
        }

        //Get the player's vertical velocity for animator blend tree
        if (PlayerVariableSet.IsAlive == true)
        {
            PlayerAnimator.SetFloat("Blend", PlayerVariableSet.VS.PlayerRigidBody.velocity.y);
        }
        //Set the animator variable for player being on ground or in air
        if(PlayerVariableSet.IsOnGround == true)
        {
            PlayerAnimator.SetBool("InAir", false);
        }
        else
        {
            PlayerAnimator.SetBool("InAir", true);
        }

        //Flip the player depending on what key was pressed last and set animator variable for whether the player is moving
        if (Input.GetKey(KeyCode.D))
        {
            PlayerAnimator.SetBool("PlayerIsMoving", true);
            PlayerVariableSet.VS.PlayerGameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            PlayerAnimator.SetBool("PlayerIsMoving", true);
            PlayerVariableSet.VS.PlayerGameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            PlayerAnimator.SetBool("PlayerIsMoving", false);
        }

        //Set the animator variable for whether the player is crouched or not
        if (Input.GetKey(KeyCode.S))
        {
            PlayerAnimator.SetBool("PlayerIsCrouched", true);
        }
        else
        {
            PlayerAnimator.SetBool("PlayerIsCrouched", false);
        }

        //set the animator variable for the player's ground slam action
        if(PlayerVariableSet.GroundSlamAction == true)
        {
            PlayerAnimator.SetBool("PlayerGroundSlamming",true);
        }
        else
        {
            PlayerAnimator.SetBool("PlayerGroundSlamming", false);
        }

        //changes player sprite colour depending on what state they are in
        if (PlayerVariableSet.IsImmune == true && PlayerVariableSet.GroundSlamAction == false)
        {
            PlayerVariableSet.VS.PlayerSprite.color = Color.red;
        }
        else if (PlayerVariableSet.ShotTaken == true)
        {
            PlayerVariableSet.VS.PlayerSprite.color = Color.cyan;
        }
        else if (PlayerVariableSet.GroundSlamAction == true)
        {
            PlayerVariableSet.VS.PlayerSprite.color = Color.green;
        }
        else
        {
            PlayerVariableSet.VS.PlayerSprite.color = Color.white;
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            PlayerVariableSet.VS.PlayerSprite.color = Color.red;
        }
        if (collision.tag == "Hazard")
        {
            PlayerVariableSet.VS.PlayerSprite.color = Color.red;
        }
    }
}
