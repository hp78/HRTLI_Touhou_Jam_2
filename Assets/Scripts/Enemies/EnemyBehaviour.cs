using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public bool canMove;

    public BoolVariable bvGamePaused;
    public BoolVariable bvGameRunning;
    public BaseEnemyStats enemyStats;
    public Transform bulletDrop;
    public Transform player;
    public Transform explosion;
    public Transform bulletPF;

    public enum PatternTypeFire { NORMAL, COMPLEX, BOTH}

    public PatternTypeFire patternType = PatternTypeFire.NORMAL;

    Rigidbody2D rigidbody2d;

    Vector2 playerDirection;

    float fireCD;





    // Use this for initialization
    void Start()
    {

        fireCD = enemyStats.fireRateCooldown;
        rigidbody2d = GetComponent<Rigidbody2D>();

        //player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //
        if (bvGamePaused.data || !bvGameRunning.data)
        {
            rigidbody2d.velocity = Vector2.zero;
            return;
        }


        if (player)
        {
            playerDirection = player.transform.position - this.transform.position;
            Shoot();
        }

        if(canMove)
        {
            Move();
        }


        fireCD -= Time.deltaTime;
    }

    void Shoot()
    {

        if (fireCD < 0.0f && playerDirection.magnitude < enemyStats.range)
        {
            Vector3 facing = player.transform.position - transform.position;
            float playerAngle = Mathf.Atan2(facing.y, facing.x) * Mathf.Rad2Deg;

            switch(patternType)
            {
                case PatternTypeFire.NORMAL:
                    BulletFactory.bulletFactoryInstance.ShootAt(this.transform, player.transform, enemyStats.bulletPattern, false);
                    //BulletFactory.bulletFactoryInstance.Shoot(this.transform, playerAngle, enemyStats.bulletPattern, false);
                    break;
                case PatternTypeFire.COMPLEX:
                    BulletFactory.bulletFactoryInstance.ComplexShoot( this.transform, playerAngle, enemyStats.complexBulletPattern, false);
                    break;


                default:break;
            }



            fireCD = enemyStats.fireRateCooldown;
        }

    }

    void Move()
    {

        rigidbody2d.velocity = playerDirection.normalized * enemyStats.movespeed;
        if (playerDirection.magnitude < enemyStats.range)
        {

            rigidbody2d.velocity = Vector2.zero;
            rigidbody2d.velocity = playerDirection.normalized * enemyStats.movespeed *0.25f;

        }

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            player = other.transform;

        else if(other.tag == "PlayerBullet")
        {
            SoundManager.Instance.GetHitSound();
            bulletDrop.GetComponent<SpriteRenderer>().sprite = bulletDrop.GetComponent<BulletDrop>().bulletPattern.dropIcon;
            Instantiate(bulletDrop, this.transform.position, Quaternion.identity);
            Instantiate(explosion, this.transform.position, Quaternion.identity);
            SoundManager.Instance.ExplosionSound();
            this.GetComponent<Collider2D>().enabled = false;
            this.gameObject.SetActive(false);
            Destroy(this.gameObject, 2f);
        }
    }


}
