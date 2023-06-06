using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioSource[] SFX_Sources;
    public AudioSource[] BGM_Sources;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(gameObject);
        }
    }

	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    
    public void SelectSound()
    {
        SFX_Sources[0].Play();
    }

    public void DeSelectSound()
    {
        SFX_Sources[1].Play();
    }

    public void GetHitSound()
    {
        SFX_Sources[2].Play();
    }

    public void PickupSound()
    {
        SFX_Sources[3].Play();
    }

    public void ShootSound()
    {
        SFX_Sources[4].Play();
    }

    public void ExplosionSound()
    {
        SFX_Sources[5].Play();
    }

    public void SwitchToMagus()
    {
        foreach(AudioSource source in BGM_Sources)
        {
            source.Stop();
        }
        BGM_Sources[1].Play();
    }

    public void SwitchToCasket()
    {
        foreach (AudioSource source in BGM_Sources)
        {
            source.Stop();
        }
        BGM_Sources[0].Play();
    }

    public void SwitchToMenuTheme()
    {
        foreach (AudioSource source in BGM_Sources)
        {
            source.Stop();
        }
        BGM_Sources[2].Play();
    }

}
