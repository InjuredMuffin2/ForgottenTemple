using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    //Controls the Player's ability to jump
    //Updates variables that keep track of certain aspects of the player's movement such as if they are on the ground and jump height

    public PhysicsMaterial2D PlayerPlatformMaterial;
    public PhysicsMaterial2D PlayerGeneralMaterial;
    public void Update()
    {
        //checks for ground using raytrace,
        //changes player material if on a moving platform so they stay on it instead of sliding off
        bool RayHitGround = Physics2D.Raycast(PlayerVariableSet.VS.GroundRayPosition.position, Vector2.down, 0.01f, PlayerVariableSet.VS.GroundLayer);
        bool RayHitPlatform = Physics2D.Raycast(PlayerVariableSet.VS.GroundRayPosition.position, Vector2.down, 0.01f, PlayerVariableSet.VS.PlatformLayer);

        //if the ray hits something in the ground layer, set the IsOnGround bool to true so other scripts can react accordingly
        if (RayHitGround == true)
        {
            PlayerVariableSet.IsOnGround = true;
            PlayerVariableSet.VS.PlayerRigidBody.sharedMaterial = PlayerGeneralMaterial;
        }

        //if the ray hits something in the platform layer while the player isnt moving, set the IsOnGround bool to true and change player material so they stay with the platform
        else if (RayHitPlatform == true && Input.GetAxisRaw("Horizontal") == 0)
        {
            PlayerVariableSet.IsOnGround = true;
            PlayerVariableSet.VS.PlayerRigidBody.sharedMaterial = PlayerPlatformMaterial;
        }
        else if (RayHitPlatform == true && Input.GetAxisRaw("Horizontal") != 0)
        {
            PlayerVariableSet.IsOnGround = true;
            PlayerVariableSet.VS.PlayerRigidBody.sharedMaterial = PlayerGeneralMaterial;
        }
        else
        {
            PlayerVariableSet.IsOnGround = false;
            PlayerVariableSet.VS.PlayerRigidBody.sharedMaterial = PlayerGeneralMaterial;
        }

        // controls how high the player jumps based on if they are crouched or not
        if (Input.GetKeyDown(KeyCode.Space) && PlayerVariableSet.IsOnGround == true && PlayerVariableSet.IsAlive == true)
        {
            PlayerVariableSet.VS.PlayerRigidBody.velocity = new Vector2(0, PlayerVariableSet.JumpStrength);
        }
        if (PlayerVariableSet.IsCrouched == 1)
        {
            PlayerVariableSet.JumpStrength = 8;
        }
        if (PlayerVariableSet.IsCrouched == 0)
        {
            PlayerVariableSet.JumpStrength = 16;
        }
    }
}
