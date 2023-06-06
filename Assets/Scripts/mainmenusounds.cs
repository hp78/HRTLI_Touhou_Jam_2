using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainmenusounds : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            SoundManager.Instance.SelectSound();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SoundManager.Instance.SelectSound();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            SoundManager.Instance.SelectSound();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            SoundManager.Instance.DeSelectSound();
        }
    }
}
