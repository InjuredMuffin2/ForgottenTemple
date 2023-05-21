using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{
    //Controls the Player's ability to crouch and lets the player "Ground Slam" if crouched in the air
    //Updates variables that keep track of certain aspects of the player's movement such as if they are crouched
    public void Update()
    {
        //player moves slower when crouched
        switch (PlayerVariableSet.IsCrouched)
        {
            case 0:
                PlayerVariableSet.PlayerSpeed = 4;
                break;
            case 1:
                PlayerVariableSet.PlayerSpeed = 1.5f;
                break;
        }

        //player crouches by pressing S
        if (Input.GetKey(KeyCode.S) == true)
        {
            PlayerVariableSet.IsCrouched = 1;
        }
        else
        {
            PlayerVariableSet.IsCrouched = 0;
        }

        //pressing S in the air instead makes the player ground slam, ollowing them to damage enemies by jumping on them
        //this means they have to intentionally land on an enemy and they will still likely take damage if they land on an enemy on accident
        if (Input.GetKeyDown(KeyCode.S) && PlayerVariableSet.IsOnGround == false && PlayerVariableSet.GroundSlamAction == false)
        {
            //make the player dash downwards if crouching in the air, and set the GroundSlamAction bool to true so ther scripts can react accordingly
            PlayerVariableSet.GroundSlamAction = true;
            PlayerVariableSet.IsImmune = true;
            PlayerVariableSet.VS.PlayerRigidBody.velocity = new Vector2(0, -PlayerVariableSet.JumpStrength);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //The player can't be doing this action when in contact with ground or any other collider
        PlayerVariableSet.GroundSlamAction = false;
        PlayerVariableSet.IsImmune = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //throws the player back into the air after jumping on an enemy
        if (collision.tag == "Enemy" && PlayerVariableSet.GroundSlamAction == true)
        {
            PlayerVariableSet.VS.PlayerRigidBody.velocity = new Vector2(0, 0.5f * PlayerVariableSet.JumpStrength);
            PlayerVariableSet.Score = PlayerVariableSet.Score + 10;
        }
    }
}
