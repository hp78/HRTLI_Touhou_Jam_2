using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OniBulletSpray : MonoBehaviour {

    public BulletPattern sprayPattern;

    Transform chen;

    float cooldown;

	// Use this for initialization
	void Start () {


        chen = GameObject.Find("Chen").transform;

	}
	
	// Update is called once per frame
	void Update () {

        float d = Random.Range(-180, 180);

        if (cooldown < 0.0f)
        {
            Vector3 facing = chen.transform.position - transform.position;
            float chenangle = Mathf.Atan2(facing.y, facing.x) * Mathf.Rad2Deg;
            BulletFactory.bulletFactoryInstance.Shoot(this.transform, d + chenangle, sprayPattern, false);
            cooldown = 0.2f;
        }

        cooldown -= Time.deltaTime;

	}
}
