using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Controls the Player's horizontal Movement
    //Updates variables that keep track of certain aspects of the player's movement such as the direction they are facing

    public void Update()
    {
        if(PlayerVariableSet.IsAlive == true)
        {
            PlayerVariableSet.HorizontalDirection = Input.GetAxisRaw("Horizontal");

            //This is here to help other scripts with player directionality
            if (PlayerVariableSet.HorizontalDirection == 1)
            {
                PlayerVariableSet.FacingRight = 1;
            }
            else if (PlayerVariableSet.HorizontalDirection == -1)
            {
                PlayerVariableSet.FacingRight = 0;
            }
        }
    }
    public void FixedUpdate()
    {
        if (PlayerVariableSet.IsAlive == true)
        {
            PlayerVariableSet.VS.PlayerRigidBody.velocity = new Vector2(PlayerVariableSet.HorizontalDirection * PlayerVariableSet.PlayerSpeed, PlayerVariableSet.VS.PlayerRigidBody.velocity.y);
        }
        else 
        {
            PlayerVariableSet.VS.PlayerRigidBody.velocity = Vector2.zero; 
        }
    }
}
