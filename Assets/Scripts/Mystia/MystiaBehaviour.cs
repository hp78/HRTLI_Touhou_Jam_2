using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MystiaBehaviour : MonoBehaviour {

    public float health;
    public int currPattern;

    public BulletPattern pattern1a;
    public BulletPattern pattern1b;

    public BulletComplexPattern pattern2a;
    public BulletComplexPattern pattern2b;

    public BulletComplexPattern pattern3a;
    public BulletComplexPattern pattern3b;

    public Transform player;

    public BoolVariable bvGamePaused;
    public BoolVariable bvGameRunning;

    public float cd1;
    public float cd2;
    public float cd3;
    

    float phase1cd;
    float phase2cd;
    float phase3cd;

    public bool start = false;
    bool moving = true;
    bool down = true;
    bool end = false;

    public SpriteRenderer sprite;
    public Collider2D col;
    float invulTime;

    public Transform[] waypoints;
    int targetpt;

    public DialogueController dialogControl;


    public Image hp;
    public Animator cutin;

    // Use this for initialization
    void Start () {

        player = GameObject.FindGameObjectWithTag("Player").transform;
        //targetpt = waypoints[Random.Range(0, waypoints.Length)];

    }

    // Update is called once per frame
    void Update () {


        if (bvGamePaused.data || !bvGameRunning.data)
            return;

        if (end && bvGameRunning.data)
        {
            SceneManager.LoadScene("MainMenu");
            SoundManager.Instance.SwitchToMenuTheme();


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
                switch (currPattern)
                {
                    case 1: Pattern1(); break;
                    case 2: Pattern2(); break;
                    case 3: Pattern3(); break;

                }

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

        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    Pattern1();
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    Pattern2();

        //}
        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    Pattern3();

        //}
    }

    public void Pattern1()
    {
        if (phase1cd < 0.0f)
        {
            BulletFactory.bulletFactoryInstance.Shoot(this.transform, 90.0f, pattern1a, false);
            BulletFactory.bulletFactoryInstance.Shoot(this.transform, 90.0f, pattern1b, false);
            BulletFactory.bulletFactoryInstance.Shoot(this.transform, 270.0f, pattern1a, false);
            BulletFactory.bulletFactoryInstance.Shoot(this.transform, 270.0f, pattern1b, false);
            phase1cd = cd1;
        }
        phase1cd -= Time.deltaTime;

    }

    public void Pattern2()
    {
        if (phase2cd < 0.0f)
        {
            Vector3 facing = player.transform.position - transform.position;
            float playerAngle = Mathf.Atan2(facing.y, facing.x) * Mathf.Rad2Deg;

            BulletFactory.bulletFactoryInstance.ComplexShoot(this.transform, playerAngle + 90f, pattern2a, false);
            BulletFactory.bulletFactoryInstance.ComplexShoot(this.transform, playerAngle - 90f, pattern2b, false);
            phase2cd = cd2;

        }
        phase2cd -= Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, waypoints[targetpt].position, 2 * Time.deltaTime);
        if (Vector2.Distance(transform.position, waypoints[targetpt].position) < 0.2f)
        {
            ++targetpt;
            if (targetpt == waypoints.Length)
                targetpt = 0;
        }

    }
    public void Pattern3()
    {
        if (phase3cd < 0.0f)
        {
            Vector3 facing = player.transform.position - transform.position;
            float playerAngle = Mathf.Atan2(facing.y, facing.x) * Mathf.Rad2Deg;

            BulletFactory.bulletFactoryInstance.ComplexShoot(this.transform, playerAngle - 135f, pattern3a, false);
            BulletFactory.bulletFactoryInstance.ComplexShoot(this.transform, playerAngle + 180f, pattern3b, false);
            phase3cd = cd3;
        }
        phase3cd -= Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, player.position, 1.5f * Time.deltaTime);

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet" && start)
        {
            --health;
            invulTime = 1.5f;

        }
    }

    public void End()
    {
        dialogControl.EnableDialogue();
        end = true;
    }
    void Unpause()
    {
        bvGameRunning.data = true;

    }
}
