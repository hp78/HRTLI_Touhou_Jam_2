using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    //
    GameObject player;
    PlayerController playerController;

    //
    Vector3 eventPos;

    //
    public bool isFollowingPlayer = true;
    bool isPanning = false;

    // Use this for initialization
    void Start()
    {
        //
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //
        if (isPanning)
        {
            if (transform.parent == null)
                PanToEvent();
            else
                PanToPlayer();
        }
        else
        {
            //PanPlayerMovement();
        }
    }

    //
    void PanPlayerMovement()
    {
        // 
        float posX = 0.0f;

        //
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            posX += -1.0f;

            //
            if (transform.localPosition.x > 0.0f)
            {
                transform.localPosition = new Vector3(Mathf.Lerp(transform.localPosition.x, posX, 1.0f * Time.deltaTime),
                    transform.localPosition.y,
                    transform.localPosition.z);
            }
            else
            {
                transform.localPosition = new Vector3(Mathf.Lerp(transform.localPosition.x, posX, 0.5f * Time.deltaTime),
                    transform.localPosition.y,
                    transform.localPosition.z);
            }

        }

        //
        if (Input.GetKey(KeyCode.RightArrow))
        {
            posX += 1.0f;


            //
            if (transform.localPosition.x < -0.0f)
            {
                transform.localPosition = new Vector3(Mathf.Lerp(transform.localPosition.x, posX, 1.0f * Time.deltaTime),
                    transform.localPosition.y,
                    transform.localPosition.z);
            }
            else
            {
                transform.localPosition = new Vector3(Mathf.Lerp(transform.localPosition.x, posX, 0.5f * Time.deltaTime),
                    transform.localPosition.y,
                    transform.localPosition.z);
            }

        }

        // 
        float posY = 0.0f;

        //
        if (Input.GetKey(KeyCode.UpArrow))
        {
            posY += 1.0f;

            //
            if (transform.localPosition.y > 0.0f)
            {
                transform.localPosition = new Vector3(transform.localPosition.x,
                    Mathf.Lerp(transform.localPosition.y, posY, 1.0f * Time.deltaTime),
                    transform.localPosition.z);
            }
            else
            {
                transform.localPosition = new Vector3(transform.localPosition.x,
                    Mathf.Lerp(transform.localPosition.y, posY, 0.5f * Time.deltaTime),
                    transform.localPosition.z);
            }

        }

        //
        if (Input.GetKey(KeyCode.DownArrow))
        {
            posY += -1.0f;


            //
            if (transform.localPosition.y > 0.0f)
            {
                transform.localPosition = new Vector3(transform.localPosition.x,
                    Mathf.Lerp(transform.localPosition.y, posY, 1.0f * Time.deltaTime),
                    transform.localPosition.z);
            }
            else
            {
                transform.localPosition = new Vector3(transform.localPosition.x,
                    Mathf.Lerp(transform.localPosition.y, posY, 0.5f * Time.deltaTime),
                    transform.localPosition.z);
            }
        }
    }

    public void FocusCameraOnEvent(Vector3 nEventPos)
    {
        // detach from parent
        isFollowingPlayer = false;
        transform.parent = null;
        isPanning = true;
        eventPos = nEventPos;
    }

    public void FocusCameraOnPlayer()
    {
        // detach from parent
        isFollowingPlayer = true;
        transform.parent = player.transform;
        isPanning = true;
    }

    void PanToEvent()
    {
        //
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, eventPos.x, 1.0f * Time.deltaTime),
            Mathf.Lerp(transform.position.y, eventPos.y, 1.5f * Time.deltaTime),
            transform.position.z);

    }

    void PanToPlayer()
    {
        //
        transform.localPosition = new Vector3(Mathf.Lerp(transform.localPosition.x, 0.0f, 3.0f * Time.deltaTime),
            Mathf.Lerp(transform.localPosition.y, 0.0f, 6.0f * Time.deltaTime),
            transform.localPosition.z);

        if (Mathf.Abs(transform.localPosition.x) < 0.1f)
        {
            transform.localPosition = new Vector3(0.0f, 0.0f, transform.localPosition.z);
            isPanning = false;
        }
    }
}
