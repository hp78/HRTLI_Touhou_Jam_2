using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageTransitionScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetTimeScaleZero()
    {
        Time.timeScale = 0;
    }

    public void SetTimeScaleOne()
    {
        Time.timeScale = 1;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        SoundManager.Instance.SwitchToMenuTheme();
    }

    public void LoadStage1()
    {

    }

    public void LoadStage2()
    {

    }

    public void LoadStage3()
    {

    }

    public void LoadStage4()
    {

    }

    public void LoadStage5()
    {

    }

    public void LoadStage6()
    {

    }

    public void LoadEnding()
    {

    }
}
