using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirnoIceBullet : MonoBehaviour {


    public BulletPattern iceShards;
    bool isPlayer= false;
    public float cooldown;
    float cd;

	// Use this for initialization
	void Start () {

        if (transform.parent.tag == "PlayerBullet")
            isPlayer = true;
        BulletFactory.bulletFactoryInstance.Shoot(this.transform, 0.0f, iceShards, isPlayer);
    }
	
	// Update is called once per frame
	void Update () {

    }
}
