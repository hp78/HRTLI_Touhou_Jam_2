using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticles : MonoBehaviour
{
    public float destroyTimer;

	// Use this for initialization
	void Start ()
    {
        DestroyObject(gameObject, destroyTimer);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
