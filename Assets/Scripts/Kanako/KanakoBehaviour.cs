using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KanakoBehaviour : MonoBehaviour {

    public float health;
    public int currPattern;

    public BulletPattern pattern1a;
    public BulletPattern pattern1b;

    public Transform pt1a;
    public Transform pt1b;

    public BulletComplexPattern pattern2a;
    public BulletComplexPattern pattern2b;


    public BulletComplexPattern pattern3a;
    public BulletComplexPattern pattern3b;



    public Transform pt3a;
    public Transform pt3b;
    public Transform pt3c;
    public Transform pt3d;

    public Transform player;
    public SpriteRenderer sprite;
    public Collider2D col;

    public BoolVariable bvGamePaused;
    public BoolVariable bvGameRunning;


    public Image hp;

    float invulTime;


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


    public Animator cutin;
    public DialogueController dialogControl;


    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update () {

        if (bvGamePaused.data || !bvGameRunning.data)
            return;

        if (end && bvGameRunning.data)
        {
            SceneManager.LoadScene("Level 6");
            SoundManager.Instance.SwitchToCasket();
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

    

    }
    public void Pattern1()
    {
        if (phase1cd < 0.0f)
        {
            BulletFactory.bulletFactoryInstance.Shoot(pt1a, 0.0f, pattern1a, false);
            BulletFactory.bulletFactoryInstance.Shoot(pt1b, 0.0f, pattern1b, false);
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

        BulletFactory.bulletFactoryInstance.ComplexShoot(this.transform, playerAngle  +45f, pattern2a, false);
        BulletFactory.bulletFactoryInstance.ComplexShoot(this.transform, playerAngle  -45f, pattern2a, false);
        BulletFactory.bulletFactoryInstance.ComplexShoot(this.transform, playerAngle  +90f, pattern2a, false);
        BulletFactory.bulletFactoryInstance.ComplexShoot(this.transform, playerAngle  -90f, pattern2a, false);
        BulletFactory.bulletFactoryInstance.ComplexShoot(this.transform, playerAngle , pattern2a, false);

        BulletFactory.bulletFactoryInstance.ComplexShoot(this.transform, playerAngle  , pattern2b, false);
        BulletFactory.bulletFactoryInstance.ComplexShoot(this.transform, playerAngle  - 45f, pattern2b, false);
        BulletFactory.bulletFactoryInstance.ComplexShoot(this.transform, playerAngle  + 45f, pattern2b, false);
        BulletFactory.bulletFactoryInstance.ComplexShoot(this.transform, playerAngle  - 90f, pattern2b, false);
        BulletFactory.bulletFactoryInstance.ComplexShoot(this.transform, playerAngle  + 90f, pattern2b, false);
            phase2cd = cd2;

        }
        phase2cd -= Time.deltaTime;

    }

    public void Pattern3()
    {
        if (phase3cd < 0.0f)
        {
            Vector3 facing = player.transform.position - transform.position;
        float playerAngle = Mathf.Atan2(facing.y, facing.x) * Mathf.Rad2Deg;

        BulletFactory.bulletFactoryInstance.ComplexShoot(pt3a, playerAngle , pattern3b, false);
        BulletFactory.bulletFactoryInstance.ComplexShoot(pt3b, playerAngle , pattern3b, false);
        BulletFactory.bulletFactoryInstance.ComplexShoot(pt3c, playerAngle , pattern3b, false);
        BulletFactory.bulletFactoryInstance.ComplexShoot(pt3d, playerAngle , pattern3b, false);
        BulletFactory.bulletFactoryInstance.ComplexShoot(this.transform, playerAngle, pattern3a, false);
            phase3cd = cd3;

        }
        phase3cd -= Time.deltaTime;


    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet" && start)
        {
            --health;
            invulTime = 1.5f;

        }
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
