using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    //
    public BoolVariable bvGamePause;
    public BoolVariable bvClearedNormal;
    public BoolVariable bvGameRunning;

    public Animator pauseAnimator;

	// Use this for initialization
	void Start () {
        bvGamePause.data = false;
        bvGameRunning.data = true;
    }
	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            bvGamePause.data = !bvGamePause.data;

            if(bvGamePause.data)
            {
                Time.timeScale = 0;
                pauseAnimator.CrossFade("Pause",0.25f);
            }
            else
            {
                pauseAnimator.CrossFade("Unpause",0.25f);
            }
        }
	}
}
