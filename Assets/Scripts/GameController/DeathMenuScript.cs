using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenuScript : MonoBehaviour {

    public BoolVariable bvPlayerAlive;
    bool isActivated = false;
    Animator animator;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {
		if(!isActivated && !bvPlayerAlive.data)
        {
            isActivated = true;
            animator.Play("DeathMenu");
        }
	}

    public void GoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        SoundManager.Instance.SwitchToMenuTheme();
    }
}
