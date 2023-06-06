using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public bool isChest;
    public Transform[] bulletDrops;
    public Transform[] FairySpawns;
    public GameObject GenericDrop;
    public GameObject Particles;


	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isChest)
        {
            if (collision.tag == "PlayerBullet")
            {
                GetComponent<BoxCollider2D>().enabled = false;

                for (int i = 0; i < bulletDrops.Length; i++)
                {
                    GenericDrop.GetComponent<BulletDrop>().bulletPattern = bulletDrops[i].GetComponent<BulletDrop>().bulletPattern;

                    GameObject dropped = Instantiate(GenericDrop, transform.position, transform.rotation);

                    dropped.GetComponent<Rigidbody2D>().AddForce
                        (new Vector2(Random.Range(-2.5f, 2.5f), Random.Range(-2.5f, 2.5f)), ForceMode2D.Impulse);

                    StartCoroutine("Timer", dropped);
                }

                for(int i = 0; i < FairySpawns.Length; i++)
                {
                    Instantiate(FairySpawns[i], this.transform.position, Quaternion.identity);
                }

                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                Instantiate(Particles, gameObject.transform.position, gameObject.transform.rotation);
                StartCoroutine("Ender");
            }
        }

        else
        {
            if (collision.tag == "PlayerBullet"|| collision.CompareTag("EnemyBullet"))
            {
                SoundManager.Instance.ExplosionSound();
                this.gameObject.SetActive(false);
                Instantiate(Particles, gameObject.transform.position, gameObject.transform.rotation);
            }
        }
        
    }

    IEnumerator Timer(GameObject Dropped)
    {
        yield return new WaitForSeconds(0.2f);
        Dropped.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    IEnumerator Ender()
    {
        yield return new WaitForSeconds(0.3f);
        SoundManager.Instance.ExplosionSound();
        gameObject.SetActive(false);
    }



}
