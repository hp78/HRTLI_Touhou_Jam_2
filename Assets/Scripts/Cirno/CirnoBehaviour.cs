using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CirnoBehaviour : MonoBehaviour {


    public float health;
    public int currPattern;

    public Transform[] waypoints;

    public BulletPattern pattern1a;
    public BulletPattern pattern1b;

    public BulletPattern pattern2;

    public BulletPattern pattern3;

    public Transform player;

    public BoolVariable bvGamePaused;
    public BoolVariable bvGameRunning;
   
    float phase3cd;
    float phase1cd;

    public bool start = false;
    bool moving = true;
    bool down = true;
    bool end = false;

    bool firePattern;
    int targetpt = 0;
    Transform targetpt2;

    public SpriteRenderer sprite;
    public Collider2D col;
    float invulTime;

    public Image hp;
    public Animator cutin;

    public DialogueController dialogControl;

    // Use this for initialization
    void Start () {
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
        targetpt2 = waypoints[Random.Range(0, waypoints.Length)];

    }

    // Update is called once per frame
    void Update () {

        if (bvGamePaused.data || !bvGameRunning.data)
            return;

        if(end && bvGameRunning.data)
        {
            SceneManager.LoadScene("Level 3");
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
                switch (currPattern)
                {
                    case 1: Phase1(); break;
                    case 2: Phase2(); break;
                    case 3: Phase3(); break;

                }

                phase1cd -= Time.deltaTime;
                phase3cd -= Time.deltaTime;
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
                firePattern = false;

                StopAllCoroutines();
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
        BulletFactory.bulletFactoryInstance.ShootAt(this.transform, player, pattern1b, false);

    }



    IEnumerator Pattern2()
    {
        yield return new WaitForSeconds(4f);

        for (float j = 0f; j < 150f; j += 30)
        {
            for (float i = 0f; i < 360f; i += 72f)
                BulletFactory.bulletFactoryInstance.Shoot(this.transform, i+j, pattern2, false);

            yield return new WaitForSeconds(1f);
        }

        firePattern = false;
        yield return 0;
    }

    IEnumerator Pattern3()
    {
        yield return new WaitForSeconds(4f);

        BulletFactory.bulletFactoryInstance.ShootAt(this.transform, player, pattern3, false);
        yield return new WaitForSeconds(1f);
        BulletFactory.bulletFactoryInstance.ShootAt(this.transform, player, pattern3, false);
        yield return new WaitForSeconds(1f);
        BulletFactory.bulletFactoryInstance.ShootAt(this.transform, player, pattern3, false);
        yield return new WaitForSeconds(1f);
        BulletFactory.bulletFactoryInstance.ShootAt(this.transform, player, pattern3, false);
        yield return new WaitForSeconds(1f);
        BulletFactory.bulletFactoryInstance.ShootAt(this.transform, player, pattern3, false);
        yield return new WaitForSeconds(1f);
        firePattern = false;

    }

    void Phase1()
    {

        transform.position = Vector2.MoveTowards(transform.position, waypoints[targetpt].position, 2 * Time.deltaTime);
        if (Vector2.Distance(transform.position, waypoints[targetpt].position) < 0.2f)
        {
            ++targetpt;
            if (targetpt == waypoints.Length)
                targetpt = 0;
        }
        if(phase1cd < 0.0f)
        {
            Pattern1();
            phase1cd = 5f;
        }
    }

    void Phase2()
    {

        transform.position = Vector2.MoveTowards(transform.position, targetpt2.position, 4 * Time.deltaTime);
        if (Vector2.Distance(transform.position, targetpt2.position) < 0.2f)
        {
            targetpt2 = waypoints[Random.Range(0, waypoints.Length)];

        }

        if (!firePattern)
        {
            firePattern = true;
            StartCoroutine(Pattern2());
        }
    }
    void Phase3()
    {

        if(!firePattern)
        {
            firePattern = true;
            StartCoroutine(Pattern3());
        }

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
