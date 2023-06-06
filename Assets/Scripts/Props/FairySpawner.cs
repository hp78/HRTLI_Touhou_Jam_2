using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairySpawner : MonoBehaviour {

    public float cooldown;

    float cd;

    public Transform[] fairies;
    public bool start;
    GameObject currFairy;

	// Use this for initialization
	void Start () {

        cd = Random.Range(0f, cooldown);
	}
	
	// Update is called once per frame
	void Update () {
		
        if(start && cd <0f)
        {
            int rand = Random.Range(0, fairies.Length);
            EnemyBehaviour temp =  Instantiate(fairies[rand], this.transform.position, Quaternion.identity).GetComponent<EnemyBehaviour>();

            if(temp)
                temp.player = GameObject.FindGameObjectWithTag("Player").transform;

            currFairy = temp.gameObject;
            cd = cooldown;
        }

        if(!currFairy && start)
        cd -= Time.deltaTime;

	}
}
