using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MikoExplodeBullet : MonoBehaviour {

    public BulletPattern blueBulletPattern;

    public float timer;

    public bool stay = false;

    bool shot=false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if(timer < 0.0f  && !shot )
        {
            if (!stay)
            {
                BulletFactory.bulletFactoryInstance.Shoot(this.transform, 0.0f, blueBulletPattern, false);
                this.gameObject.SetActive(false);
            }

            else
            {
                StartCoroutine(LaserPattern());
            }
            shot = true;
        }

        timer -= Time.deltaTime;
	}

    IEnumerator LaserPattern()
    {
        for (int i = 0; i < 5; ++i)
        {
            BulletFactory.bulletFactoryInstance.Shoot(this.transform, 0.0f, blueBulletPattern, false);
            yield return new WaitForSeconds(1f);
        }

        yield return 0;
    }
}
