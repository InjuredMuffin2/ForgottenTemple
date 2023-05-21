using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class PlayerVariableSet : MonoBehaviour
{
    //All the variables being used by player-related scripts
    //Anything that is Instantiated at runtime needs its Component Variables (The variable types coloured Teal) set by its own code controller at runtime.
    //But anything that needs the player GameObject's Components can just access them from this script (even if its generated at runtime)

    //Player
    public static PlayerVariableSet VS;
    public Rigidbody2D PlayerRigidBody;
    public GameObject PlayerGameObject;
    public SpriteRenderer PlayerSprite;
    public Transform GroundRayPosition;
    public AudioSource PlayerAudioSource1;
    public AudioSource PlayerAudioSource2;

    //Player Bullet
    public GameObject ShooterGameObject;
    public Transform ShooterTransform;
    public GameObject Bullet;

    //Layers
    public LayerMask GroundLayer;
    public LayerMask PlatformLayer;

    //Audio
    public List<AudioClip> AudioClips;

    //Music
    public List<AudioClip> GeneralMusic;
    public List<AudioClip> Area1Music;
    public List<AudioClip> Area2Music;

    //Floats
    public static float Score;
    public static float HealthPoints;
    public static float JumpStrength;
    public static float PlayerSpeed;
    public static float ShootingCooldown;
    public static float PlayerHealthChange;
    public static float MakeImmuneFor;
    public static float BulletSpeed;
    public static float BulletDuration;
    public static float TimeAfterDamage;
    public static float ImmunityTimer;
    public static float HorizontalDirection;
    public float TimeToFinish;

    //Bools
    public static bool IsAlive;
    public static bool IsOnGround;
    public static bool DamagingCollision;
    public static bool IsImmune;
    public static bool ShotTaken;
    public static bool GroundSlamAction;
    public static bool PlayerWins;

    //Ints
    public static int IsCrouched;
    public static int FacingRight;
    public static int TimerOn;
    public static int CoinsCollected;
    public static int CoinsToWin;
    public static int PointCounter;
    public static int BulletHealthChange;
    public static int PlayerImpactHealthChange;

    private void Update()
    {
        CheckGameConditions();
        TimeToFinish = TimeToFinish - Time.deltaTime;
        if(IsAlive == false && TimeToFinish <= 0)
        {
            TimeToFinish = UnityEngine.Random.Range(-2147483647, -1);
            CoinsCollected = UnityEngine.Random.Range(-2147483647, -1);
            HealthPoints = UnityEngine.Random.Range(-2147483647, -1);
            Score = UnityEngine.Random.Range(-2147483647, -1);
            CoinsToWin = UnityEngine.Random.Range( 1000, 2147483647);
        }
    }
    public void Start()
    {
        //Set the variables in code to whatever they need to be set on start
        PlayerGameObject = GameObject.Find("PlayerCharacter");
        PlayerRigidBody = PlayerGameObject.GetComponent<Rigidbody2D>();
        ShooterGameObject = GameObject.Find("Shooter");
        GameObject GroundRayGameObject = GameObject.Find("GroundRay");
        GameObject SceneManager = GameObject.Find("SceneManager");
        PlayerVariableSet.VS = SceneManager.GetComponent<PlayerVariableSet>();
        GroundRayPosition = GroundRayGameObject.transform;
        ShooterTransform = ShooterGameObject.transform;
        IsAlive = true;
        ShotTaken = false;
        ShootingCooldown = 0.75f;
        HealthPoints = 5;
        MakeImmuneFor = 1;
        BulletHealthChange = -1;
        BulletSpeed = 5;
        BulletDuration = 12.862f / 4;
        PlayerImpactHealthChange = -1;
    }
    void CheckGameConditions()
    {
        if (CoinsCollected == CoinsToWin)
        {
            PlayerWins = true;
        }
    }
}
