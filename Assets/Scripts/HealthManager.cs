using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    //Updates the Player's Health based off of the state of other variables
    //Controls immunity frames after player takes damage

    public void Update()
    {
        if (PlayerVariableSet.HealthPoints <= 0)
        {
            PlayerVariableSet.IsAlive = false;
        }
        //If the player isn't immune, turn the timer off
        if (PlayerVariableSet.IsImmune == false)
        {
            PlayerVariableSet.TimerOn = 0;
        }
        //Only do these when the player is still alive
        if (PlayerVariableSet.IsAlive == true)
        {
            //If the player is in contact with a damage source OR the Immunity Timer has not finished counting down
            if (PlayerVariableSet.ImmunityTimer < PlayerVariableSet.MakeImmuneFor || PlayerVariableSet.DamagingCollision == true)
            {
                //starts immunity timer
                PlayerVariableSet.ImmunityTimer = PlayerVariableSet.ImmunityTimer - Time.deltaTime;
                PlayerVariableSet.IsImmune = true;
            }
            //If the Immunity Timer is done, end player Immunity and reset the timer
            if (PlayerVariableSet.ImmunityTimer <= 0 && PlayerVariableSet.DamagingCollision == false)
            {
                //restes immunity timer
                PlayerVariableSet.ImmunityTimer = PlayerVariableSet.MakeImmuneFor;
                PlayerVariableSet.IsImmune = false;
            }
            // if the player is not immune, deal damage
            if (PlayerVariableSet.ImmunityTimer <= 0 && PlayerVariableSet.DamagingCollision == true)
            {
                //deals damage 
                PlayerVariableSet.HealthPoints = PlayerVariableSet.HealthPoints + PlayerVariableSet.PlayerHealthChange;
                PlayerVariableSet.ImmunityTimer = PlayerVariableSet.MakeImmuneFor;
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Only do this if the player isn't immune
        if (PlayerVariableSet.IsImmune == false)
        {
            //Get the Enemy's damage value and deal that amount of damage to the player
            if (collision.tag == "Enemy" && PlayerVariableSet.IsAlive == true)
            {
                EnemyController EnemyInfo = collision.GetComponent<EnemyController>();
                PlayerVariableSet.PlayerHealthChange = EnemyInfo.EnemyHealthChange;
                PlayerVariableSet.DamagingCollision = true;
                PlayerVariableSet.HealthPoints = PlayerVariableSet.HealthPoints + PlayerVariableSet.PlayerHealthChange;
            }
            //Get the Hazard's damage value and deal that amount of damage to the player
            else if (collision.tag == "Hazard" && PlayerVariableSet.IsAlive == true)
            {
                HazardController HazardInfo = collision.GetComponent<HazardController>();
                PlayerVariableSet.PlayerHealthChange = HazardInfo.HazardHealthChange;
                PlayerVariableSet.DamagingCollision = true;
                PlayerVariableSet.HealthPoints = PlayerVariableSet.HealthPoints + PlayerVariableSet.PlayerHealthChange;
            }
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        //allow the DealDamageToPlayer function to do it's job by setting DamagingCollision to true and starting the timer
        if (PlayerVariableSet.IsImmune == false && collision.tag == "Enemy")
        {
            PlayerVariableSet.DamagingCollision = true;
            TimeSinceDamage();
        }
        //allow the DealDamageToPlayer function to do it's job by setting DamagingCollision to true and starting the timer
        if (PlayerVariableSet.IsImmune == false && collision.tag == "Hazard")
        {
            PlayerVariableSet.DamagingCollision = true;
            TimeSinceDamage();
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        //set damage to 0 to prevent any accidental damage (My code caused the player to take damage at the end of immunity even when they are no longer touching a damage source)
        PlayerVariableSet.DamagingCollision = false;
        PlayerVariableSet.PlayerHealthChange = 0;
    }
    void TimeSinceDamage()
    {
        //keeps track of how much time has passed since the player took damage
        switch (PlayerVariableSet.TimerOn)
        {
            case 0:
                PlayerVariableSet.TimeAfterDamage = 0;
                break;
            case 1:
                PlayerVariableSet.TimeAfterDamage = PlayerVariableSet.TimeAfterDamage + Time.deltaTime;
                break;
        }
    }
}
