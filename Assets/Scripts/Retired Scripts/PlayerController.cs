using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //PUBLIC-------------------------
    public Rigidbody2D BulletRB;
    public GameObject Bullet;
    public float HorizontalDirection;
    public bool FacingRight;
    public float HealthPoints;
    //PRIVATE------------------------
    private Rigidbody2D PlayerRB;
    private PlayerController PlayerPC;
    private GameObject ShooterGO;
    private Transform Shooter;
    private float jumpStrength;
    private float PlayerSpeed;
    private int DamageToPlayer;
    private int MakeImmuneFor;
    private bool IsAlive;
    private bool IsOnGround;
    private bool IsCrouched;
    private float ImmunityTimer;
    private bool DamagingCollision;
    private bool IsImmune;
    private float ShootingCooldown;
    private bool ShotTaken;
    private float TimeAfterDamage;
    private bool TimerOn;
    void Start()
    {
        //a list of variables that MUST be set to something other than 0/null/true or false for the script to function properly
        PlayerRB = GetComponent<Rigidbody2D>();
        PlayerPC = GetComponent<PlayerController>();
        ShooterGO = GameObject.Find("Shooter");
        Shooter = ShooterGO.transform;
        IsAlive = true;
        ShotTaken = false;
        ShootingCooldown = 0.75f;

        if (HealthPoints == 0)
        {
            HealthPoints = 5;
        }
        if (MakeImmuneFor == 0)
        {
            MakeImmuneFor = 1;
        }
    }
    void Update()
    {
        if (IsAlive == true)
        {

            //HORIZONTAL MOVEMENT------------------------------------------------------------------//
            
            //Controls Player's Horizontal Movement
            if (HorizontalDirection == 1)
            {
                FacingRight = true;
            }
            else if (HorizontalDirection == -1)
            {
                FacingRight = false;
            }

            HorizontalDirection = Input.GetAxisRaw("Horizontal");

            //CROUCHING----------------------------------------------------------------------------//

            //changes the player size and movement speed when crouching
            if (IsCrouched == false)
            {
                PlayerSpeed = 8;
            }
            else
            {
                PlayerSpeed = 3;
            }
            //Makes the player slam into the ground when pressing S in the air
            if (Input.GetKeyDown(KeyCode.S) && IsOnGround == false)
            {
                // *** give the player immunity while ground slamming ***
                PlayerRB.velocity = new Vector2(0, -jumpStrength);
            }
            //shrink the player's hitbox when crouched
            //else if not crouched, revert values to normal
            if (Input.GetKey(KeyCode.S) == true)
            {
                IsCrouched = true;
                PlayerRB.transform.localScale = new Vector3(1, 0.5f, 1);
                CircleCollider2D PlayerCircleCollider = PlayerRB.GetComponent<CircleCollider2D>();
                PlayerCircleCollider.radius = 0.25f;
            }
            else
            {
                IsCrouched = false;
                PlayerRB.transform.localScale = Vector3.one;
                CircleCollider2D PlayerCircleCollider = PlayerRB.GetComponent<CircleCollider2D>();
                PlayerCircleCollider.radius = 0.5f;
            }
            //Being crouched makes you jump lower
            if (PlayerPC.IsCrouched == true)
            {
                jumpStrength = 12;
            }
            if (PlayerPC.IsCrouched == false)
            {
                jumpStrength = 24;
            }

            //DAMAGE CONTROLS----------------------------------------------------------------------//

            //if player is in a Damage trigger, starts an immunity frame timer (counts down to 0)
            if (ImmunityTimer < MakeImmuneFor || DamagingCollision == true)
            {
                ImmunityTimer = ImmunityTimer - Time.deltaTime;
                IsImmune = true;
            }
            //"Kills" player when health drops to 0
            if (HealthPoints <= 0)
            {
                //disable the player's controls when their health
                SpriteRenderer PlayerSprite = GetComponent<SpriteRenderer>();
                PlayerSprite.enabled = false;
                IsAlive = false;
            }
            //when the player leaves the damage trigger they keep the immunity frames for a set duration, once that ends, immunity timer is stopped
            if (ImmunityTimer <= 0 && DamagingCollision == false)
            {
                ImmunityTimer = MakeImmuneFor;
                IsImmune = false;
            }
            //damages the player when they are in contact with something that deals damage
            if (ImmunityTimer <= 0 && DamagingCollision == true)
            {
                HealthPoints = HealthPoints + DamageToPlayer;
                ImmunityTimer = MakeImmuneFor;
            }

            //Checks If The Player Is On The Ground And Allows For Jumping Only If On The Ground
            //Makes the player jump only if on the ground
            if (Input.GetKeyDown(KeyCode.Space) && IsOnGround == true)
            {
                PlayerRB.velocity = new Vector2(0, jumpStrength);
            }
            
            //start a countdown after the player shoots
            if (ShotTaken == true)
            {
                ShootingCooldown = ShootingCooldown - Time.deltaTime;
            }
            //reset the countdown and let the player shoot again
            if (ShootingCooldown <= 0)
            {
                ShotTaken = false;
                ShootingCooldown = 0.75f;
            }
            //Keeps track of how long it has been since the player took damage
            //if not enough time passed since the player took damage, they cannot take more damage
            if (TimerOn == true)
            {
                TimeAfterDamage = TimeAfterDamage + Time.deltaTime;
            }
            else if (TimerOn == false)
            {
                TimeAfterDamage = 0;
            }

            //Makes the player shoot and puts shooting on a set cooldown
            if (ShotTaken == false && Input.GetKeyDown(KeyCode.Mouse0))
            {
                Instantiate(Bullet, Shooter.position, Quaternion.identity);
                ShotTaken = true;
            }

            if (IsImmune == false)
            {
                SpriteRenderer PlayerSprite = GetComponent<SpriteRenderer>();
                PlayerSprite.color = Color.white;
            }
            else if (TimeAfterDamage >= 0.9f)
            {
                SpriteRenderer PlayerSprite = GetComponent<SpriteRenderer>();
                PlayerSprite.color = Color.white;
                TimerOn = false;
            }
        }
    }
    public void FixedUpdate()
    {
        PlayerRB.velocity = new Vector2(HorizontalDirection * PlayerSpeed, PlayerRB.velocity.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "Hazard")
        {
            TimerOn = true;
            SpriteRenderer PlayerSprite = GetComponent<SpriteRenderer>();
            PlayerSprite.color = Color.red;
        }
        if (IsImmune == false)
        {
            //deals with enemy interactions with the player
            if(collision.tag == "Enemy")
            {
                EnemyController EnemyInfo = collision.GetComponent<EnemyController>();
                DamageToPlayer = EnemyInfo.EnemyHealthChange;
                DamagingCollision = true;
                HealthPoints = HealthPoints + DamageToPlayer;
            }
            //deals with hazard interactions with the player
            else if (collision.tag == "Hazard") 
            { 
                HazardController HazardInfo = collision.GetComponent<HazardController>();
                DamageToPlayer = HazardInfo.HazardHealthChange;
                DamagingCollision = true;
                HealthPoints = HealthPoints + DamageToPlayer;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            SpriteRenderer PlayerSprite = GetComponent<SpriteRenderer>();
            PlayerSprite.color = Color.red;

            if (TimerOn == true)
            {
                TimeAfterDamage = TimeAfterDamage + Time.deltaTime;
            }
            else if (TimerOn == false)
            {
                TimeAfterDamage = 0;
            }
        }
        if (collision.tag == "Hazard")
        {
            SpriteRenderer PlayerSprite = GetComponent<SpriteRenderer>();
            PlayerSprite.color = Color.red;

            if (TimerOn == true)
            {
                TimeAfterDamage = TimeAfterDamage + Time.deltaTime;
            }
            else if (TimerOn == false)
            {
                TimeAfterDamage = 0;
            }
        }
        //only allows the player to take damage if they don't currently have immunity frames
        if (IsImmune == false && collision.tag == "Enemy" || collision.tag == "Hazard")
        {
            DamagingCollision = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //stops damage once player leaves damage-causing triggers
        //sets damage to 0 in case something causes the DamagingCollision bool to stay as true even after leaving damage-causing triggers
        DamagingCollision = false;
        DamageToPlayer = 0;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //checks if the player is on the ground
        if (collision.gameObject.tag == "Ground")
        {
            IsOnGround = true;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        //prevents a glitch that cause the player to be marked as not on ground even when on ground
        if (collision.gameObject.tag == "Ground")
        {
            IsOnGround = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //when exiting collision with a ground object, sets the player status as no longer on ground
        if (collision.gameObject.tag == "Ground")
        {
            IsOnGround = false;
        }
    }
}
