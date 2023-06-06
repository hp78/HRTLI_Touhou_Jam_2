using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    //
    public BoolVariable bvGamePaused;
    public BoolVariable bvGameRunning;

    [Space(5)]
    public BoolVariable bvControlScheme;
    public BoolVariable bvPlayerAlive;
    public enum PlayerFacing {  Up, UpLeft, UpRight, Down, DownLeft, DownRight, Left , Right };
    public PlayerFacing currPlayerFacing = PlayerFacing.Down;
    public Transform hitbox;

    Vector2 currShootDirection;
    Rigidbody2D rb;
    PlayerInventory playerInv;
    Animator playerAnimator;

    public float playerSpeed = 5.0f;
    public float playerStrafeSpeed = 1.0f;
    float currSpeed = 5.0f;

    //CircleCollider2D circol;
    public float invulFrames = 0f;
    SpriteRenderer sprite;

    //For Dropping
    public GameObject Drop;
    BulletPattern PatternToDrop;

	// Use this for initialization
	void Start ()
    {
        bvPlayerAlive.data = true;

        currShootDirection = new Vector2(0, -1);
        rb = GetComponent<Rigidbody2D>();
        playerInv = GetComponent<PlayerInventory>();
        playerAnimator = GetComponent<Animator>();
        //circol = GetComponent<CircleCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        playerInv.UpdateNextShots();
        playerInv.UpdateNextBombs();
    }
	
	// Update is called once per frame
	void Update ()
    {
        PlayerInput();
        PlayerFlicker();
	}

    //
    void PlayerInput()
    {
        //
        Vector2 moveVec = new Vector2(0,0);

        if (bvGamePaused.data || !bvGameRunning.data || !bvPlayerAlive.data)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        //
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveVec += new Vector2(0, 1);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveVec -= new Vector2(0, 1);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveVec -= new Vector2(1, 0);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveVec += new Vector2(1, 0);
        }

        // Control Scheme A (Hold to strafe)
        if (bvControlScheme)
        {
            // shoot
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Shoot(currPlayerFacing);
                SoundManager.Instance.ShootSound();
            }


            if (Input.GetKeyDown(KeyCode.X))
                Bomb(currPlayerFacing);

            // focus fire
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.Space))
            {
                currSpeed = playerStrafeSpeed;
                hitbox.gameObject.SetActive(true);
            }
            else
            {
                hitbox.gameObject.SetActive(false);
                currSpeed = playerSpeed;

                //
                if (moveVec != Vector2.zero)
                    ChangeFacing(moveVec);
            }
        }

        if (moveVec == Vector2.zero)
            playerAnimator.speed = 0.0f;
        else
            playerAnimator.speed = 1.0f;

        rb.velocity = moveVec.normalized * currSpeed;
    }

    //
    void ChangeFacing(Vector2 faceVector)
    {
        playerAnimator.SetFloat("Horizontal", faceVector.x);
        playerAnimator.SetFloat("Vertical", faceVector.y);

        if (faceVector == new Vector2(0, 1))
        {
            currPlayerFacing = PlayerFacing.Up;
        }
        else if (faceVector == new Vector2(-1, 1))
        {
            currPlayerFacing = PlayerFacing.UpLeft;
        }
        else if (faceVector == new Vector2(1, 1))
        {
            currPlayerFacing = PlayerFacing.UpRight;
        }
        else if (faceVector == new Vector2(0, -1))
        {
            currPlayerFacing = PlayerFacing.Down;
        }
        else if (faceVector == new Vector2(-1, -1))
        {
            currPlayerFacing = PlayerFacing.DownLeft;
        }
        else if (faceVector == new Vector2(1, -1))
        {
            currPlayerFacing = PlayerFacing.DownRight;
        }
        else if (faceVector == new Vector2(-1, 0))
        {
            currPlayerFacing = PlayerFacing.Left;
        }
        else if (faceVector == new Vector2(1, 0))
        {
            currPlayerFacing = PlayerFacing.Right;
        }
    }

    //
    void Shoot(PlayerFacing playerFacing)
    {
        switch(playerFacing)
        {
            case PlayerFacing.Up:
                currShootDirection = new Vector2(0, 1);
                break;

            case PlayerFacing.UpLeft:
                currShootDirection = new Vector2(-1, 1);
                break;

            case PlayerFacing.UpRight:
                currShootDirection = new Vector2(1, 1);
                break;

            case PlayerFacing.Down:
                currShootDirection = new Vector2(0, -1);
                break;

            case PlayerFacing.DownLeft:
                currShootDirection = new Vector2(-1, -1);
                break;

            case PlayerFacing.DownRight:
                currShootDirection = new Vector2(1, -1);
                break;

            case PlayerFacing.Left:
                currShootDirection = new Vector2(-1, 0);
                break;

            case PlayerFacing.Right:
                currShootDirection = new Vector2(1, 0);
                break;

            default:
                break;
        }

        if (playerInv.shotList.Count > 0)
        {
            BulletFactory.bulletFactoryInstance.Shoot(transform,
                Mathf.Atan2(currShootDirection.y, currShootDirection.x) * 180 / Mathf.PI,
                playerInv.shotList[0], true);
            playerInv.shotList.RemoveAt(0);
        }
        playerInv.UpdateNextShots();
    }

    //
    void Bomb(PlayerFacing playerFacing)
    {
        switch (playerFacing)
        {
            case PlayerFacing.Up:
                currShootDirection = new Vector2(0, 1);
                break;

            case PlayerFacing.UpLeft:
                currShootDirection = new Vector2(-1, 1);
                break;

            case PlayerFacing.UpRight:
                currShootDirection = new Vector2(1, 1);
                break;

            case PlayerFacing.Down:
                currShootDirection = new Vector2(0, -1);
                break;

            case PlayerFacing.DownLeft:
                currShootDirection = new Vector2(-1, -1);
                break;

            case PlayerFacing.DownRight:
                currShootDirection = new Vector2(1, -1);
                break;

            case PlayerFacing.Left:
                currShootDirection = new Vector2(-1, 0);
                break;

            case PlayerFacing.Right:
                currShootDirection = new Vector2(1, 0);
                break;

            default:
                break;
        }

        if (playerInv.bombList.Count > 0)
        {
            BulletFactory.bulletFactoryInstance.Shoot(transform,
                Mathf.Atan2(currShootDirection.y, currShootDirection.x) * 180 / Mathf.PI,
                playerInv.bombList[0], true);
            playerInv.bombList.RemoveAt(0);
        }

        playerInv.UpdateNextBombs();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag =="EnemyBullet" && invulFrames <0.0f)
        {
            //Debug.Log("PlayerGotHitBro");
            invulFrames = 2f;
            DropShots();
        }
    }


    private void DropShots()
    {
        playerAnimator.Play("Hurt");

        SoundManager.Instance.GetHitSound();

        if(playerInv.shotList.Count < 1)
        {
            // player died
            playerAnimator.speed = 0.0f;
            bvPlayerAlive.data = false;
        }
        else if(playerInv.shotList.Count > 2)
        {
            // drops shots

            // set invul frames

            //Spawns 1 and 2 around the player

            //circol.enabled = false;

            for(int i = 0; i < 2;++i)
            {
                Drop.GetComponent<BulletDrop>().bulletPattern = playerInv.shotList[1+i];
                Drop.GetComponent<SpriteRenderer>().sprite = playerInv.shotList[1 + i].dropIcon;
                GameObject Dropped = Instantiate(Drop, transform.position, transform.rotation);
                Dropped.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(0,0.5f), Random.Range(0, 0.5f)), ForceMode2D.Impulse);
                StartCoroutine("Timer", Dropped);
            }

            for(int i =0; i < 2; ++i)
            {
                playerInv.shotList.RemoveAt(0);
            }
        }
        else if(playerInv.shotList.Count > 1)
        {
            //circol.enabled = false;

            for (int i = 0; i < 1; ++i)
            {
                Drop.GetComponent<BulletDrop>().bulletPattern = playerInv.shotList[1 + i];
                GameObject Dropped = Instantiate(Drop, transform.position, transform.rotation);
                Dropped.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-2.5f, 2.5f), Random.Range(-2.5f, 2.5f)), ForceMode2D.Impulse);
                StartCoroutine("Timer", Dropped);
            }

            for (int i = 0; i < 2; ++i)
            {
                playerInv.shotList.RemoveAt(0);
            }
        }

        else
        {
            playerInv.shotList.RemoveAt(0);
        }
        playerInv.UpdateNextShots();
    }

    IEnumerator Timer(GameObject Dropped)
    {
        yield return new WaitForSeconds(0.3f);
        Dropped.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        yield return new WaitForSeconds(1f);
       // circol.enabled = true;
    }

    void PlayerFlicker()
    {
        if (invulFrames > 0.0f)
        {
            sprite.enabled = !sprite.enabled;
           
        }
        else
        {
            sprite.enabled = true;

        }

        invulFrames -= Time.deltaTime;
    }
}
