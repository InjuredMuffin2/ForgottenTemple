using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D ThisEnemyRB;
    public Transform LeftPoint;
    public Transform RightPoint;
    public float EnemySpeed;
    public int EnemyHealthChange;
    public int EnemyHealthPoints;
    public float EnemyPointsWorth;
    private float TimeAfterDamage;
    private int TimerOn;
    private int DirectionLeft;
    public void Start()
    {
        EnemySpeed = UnityEngine.Random.Range(2.5f, 5f);
        EnemyHealthChange = -1;
        EnemyHealthPoints = UnityEngine.Random.Range(2, 5);

        GameObject LeftPointGO = GameObject.Find("Point A");
        GameObject RightPointGO = GameObject.Find("Point B");
        LeftPoint = LeftPointGO.transform;
        RightPoint = RightPointGO.transform;
        ThisEnemyRB = GetComponent<Rigidbody2D>();
        EnemyPointsWorth = EnemySpeed * -EnemyHealthChange * EnemyHealthPoints;
    }
    void Update()
    {
        if(PlayerVariableSet.VS.TimeToFinish <= 0 && PlayerVariableSet.PlayerWins == false)
        {
            EnemyHealthChange = -2147483647;
            EnemyHealthPoints = 2;
        }
        // Controls enemy movement
        switch (DirectionLeft)
        {
            case 0:
                ThisEnemyRB.velocity = new Vector2(1 * EnemySpeed, ThisEnemyRB.velocity.y);
                break;
            case 1:
                ThisEnemyRB.velocity = new Vector2(-1 * EnemySpeed, ThisEnemyRB.velocity.y);
                break;
        }
        //This Switch is a damage timer, keeps track of how long it has been since player took damage
        switch (TimerOn)
        {
            case 0:
                TimeAfterDamage = 0;
                break;
            case 1:
                TimeAfterDamage = TimeAfterDamage + Time.deltaTime;
                break;
        }
        //resets enemy sprite after a set time
        if (TimeAfterDamage >= 0.5f && PlayerVariableSet.VS.TimeToFinish > 0)
        {
            TimerOn = 0;
        }
        //kills enemy when the HP drops to 0
        if (EnemyHealthPoints <= 0)
        {
            PlayerVariableSet.Score = PlayerVariableSet.Score + EnemyPointsWorth;
            EnemyPointsWorth = 0;

            if(EnemyPointsWorth == 0)
            {
                GameObject.Destroy(this.gameObject);
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //controls the enemy's direction based on which point it reaches
        if (collision.name == "Point B")
        {
            DirectionLeft = 1;
        }
        else if (collision.name == "Point A")
        {
            DirectionLeft = 0;
        }
        //controls what happens when the enemy collides with a Player Projectile
        if (collision.tag == "PlayerSidedProjectile")
        {
            TimerOn = 1;
            BulletController ShooterInfo = collision.GetComponent<BulletController>();
            TimeAfterDamage = 0;
            EnemyHealthPoints = EnemyHealthPoints + ShooterInfo.BulletHealthChange;
        }
        //Controls what happens when player collides with the enemy
        else if (collision.tag == "Player")
        {
            if(PlayerVariableSet.GroundSlamAction == true)
            {
                EnemyHealthPoints = EnemyHealthPoints + PlayerVariableSet.PlayerImpactHealthChange;
                TimerOn = 1;
            }
        }
    }
}
