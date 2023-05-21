using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public void Update()
    {
        //if mouse is clicked, shoot bullet in direction that player is facing
        if (PlayerVariableSet.ShotTaken == false && Input.GetKeyDown(KeyCode.Mouse0) && PlayerVariableSet.PlayerWins == false)
        {
            Instantiate(PlayerVariableSet.VS.Bullet, PlayerVariableSet.VS.ShooterTransform.position, Quaternion.identity);
            PlayerVariableSet.ShotTaken = true;
        }

        //timer for shooting cooldown
        if (PlayerVariableSet.ShotTaken == true)
        {
            PlayerVariableSet.ShootingCooldown = PlayerVariableSet.ShootingCooldown - Time.deltaTime;
        }

        //end cooldown after enough time passed
        if (PlayerVariableSet.ShootingCooldown <= 0)
        {
            PlayerVariableSet.ShotTaken = false;
            PlayerVariableSet.ShootingCooldown = 0.75f;
        }
    }
}
