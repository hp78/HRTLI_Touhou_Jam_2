using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ChenBehaviour : MonoBehaviour
{

    public float health;
    public int currPattern;

    public Transform[] waypoints;

    public BulletPattern pattern1a;
    public BulletPattern pattern1b;

    public BulletPattern pattern2;

    public BulletPattern pattern3a;
    public BulletPattern pattern3b;

    Rigidbody2D rigidbody2d;
    Animator anim;
    public SpriteRenderer sprite;
    public Collider2D col;
    public Transform player;

    public Transform redOniSpawn;
    public Transform blueOniSpawn;

    public BoolVariable bvGamePaused;
    public BoolVariable bvGameRunning;

    public Image hp;

    float invulTime;

    public bool start = false;
    bool moving = true;
    bool end = false;
    bool down = true;
    bool firePattern;
    Transform target;

    public Animator cutin;

    public DialogueController dialogControl;


    // Use this for initialization
    void Start()
    {
            target = waypoints[Random.Range(0, waypoints.Length)];


        rigidbody2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        sprite = this.transform.GetChild(0).transform.GetComponent<SpriteRenderer>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (bvGamePaused.data || !bvGameRunning.data)
            return;


        if (end && bvGameRunning.data)
        {
            SceneManager.LoadScene("Level 2");
            SoundManager.Instance.SwitchToCasket();
        }

        if (start)
        {


            if (down)
            {
                hp.fillAmount += .75f * Time.deltaTime;
                if (hp.fillAmount >= 1f)
                {
                    health = 3;
                    down = false;
                    bvGameRunning.data = false;
                    invulTime = 0f;
                    cutin.Play("CutIn");
                    Invoke("Unpause", 2.5f);
                }

            }
            else
            {
                Move();
                hp.fillAmount = health / 3f;
            }


            if (invulTime > 0.0f)
            {
                sprite.enabled = !sprite.enabled;
                col.enabled = false;
            }

            else
            {
                sprite.enabled = true;
                col.enabled = true;
            }


            if (health <= 0 && !down)
            {
                
                ++currPattern;
                
                if (currPattern == 4)
                    End();
                else
                {
                    down = true;

                }
                health = 3;

            }

            invulTime -= Time.deltaTime;
        }
    }

    public void Pattern1()
    {
        BulletFactory.bulletFactoryInstance.Shoot(this.transform, 0.0f, pattern1a, false);
        BulletFactory.bulletFactoryInstance.Shoot(this.transform, 0.0f, pattern1b, false);

    }

    public void Pattern2()
    {
        BulletFactory.bulletFactoryInstance.Shoot(sprite.transform, 0.0f, pattern2, false);
    }

    public void Pattern3()
    {
        Vector3 facing = player.transform.position - transform.position;
        float playerAngle = Mathf.Atan2(facing.y, facing.x) * Mathf.Rad2Deg;

        BulletFactory.bulletFactoryInstance.Shoot(redOniSpawn, playerAngle, pattern3a, false);
        BulletFactory.bulletFactoryInstance.Shoot(blueOniSpawn, playerAngle, pattern3b, false);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag ==  "PlayerBullet" && start)
        {
            --health;
            invulTime = 1.5f;
        }
    }

    public void Move()
    {
        if (moving)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, 3 * Time.deltaTime);
            if (Vector2.Distance(transform.position, target.position)< 0.2f)
            {
                moving = false;
                firePattern = true;
                ChoosePattern();
            }
        }

        else if(!firePattern)
        {
            target = waypoints[Random.Range(0, waypoints.Length)];
            //moving = true;
        }
    }

    public void ChoosePattern()
    {
        switch(currPattern)
        {
            case 1: Pattern1(); break;
            case 2: anim.Play("Pattern2"); break;
            case 3: Pattern3(); break;
            default: break;
        }
        firePattern = false;
        Invoke("SwapToMove", 5f);
    }

    void SwapToMove()
    {
        moving = true;
    }

    void Unpause()
    {
        bvGameRunning.data = true;

    }


    public void End()
    {
        dialogControl.EnableDialogue();
        end = true;
    }
}
