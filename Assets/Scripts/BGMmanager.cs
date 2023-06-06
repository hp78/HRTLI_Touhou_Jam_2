using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMmanager : MonoBehaviour
{
    public static BGMmanager Instance;
    public  AudioSource Casket;
    public  AudioSource Magus;
    
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(this);
        }
    }

	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ChangeToCasket()
    {
        Magus.Stop();
        Casket.Play();
    }

    void ChangeToMagus()
    {
        Casket.Stop();
        Magus.Play();
    }



}
