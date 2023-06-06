using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour {

    public BoolVariable bvGamePause;
    public Button resumeBtn;
    public EventSystem eventSys;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ActivateResumeButton()
    {
        resumeBtn.enabled = true;
        eventSys.SetSelectedGameObject(resumeBtn.gameObject, new BaseEventData(eventSys));
    }

    public void RevertTimeScale()
    {
        resumeBtn.enabled = false;
        bvGamePause.data = false;
        Time.timeScale = 1;
    }

    public void GoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        SoundManager.Instance.SwitchToMenuTheme();
    }

    public void ExitGame()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else

            Application.Quit();
#endif

    }
}
