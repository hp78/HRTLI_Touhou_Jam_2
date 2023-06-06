using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

    public BoolVariable bvGamePaused;
    public BoolVariable bvGameRunning;
    public BoolVariable bvClearedNormal;
    public Button hardModeBtn;
    public Text hardModeText;

    // Use this for initialization
    void Start() {

        bvGamePaused.data = false;
        bvGameRunning.data = true;
        Time.timeScale = 1.0f;

        if (bvClearedNormal.data)
        {
            hardModeBtn.interactable = true;
            hardModeText.color = Color.white;
        }
    }

	// Update is called once per frame
	void Update () {
		
	}

    public void StartNormalMode()
    {
        SceneManager.LoadScene("Level 1");
        bvClearedNormal.data = true;
        SoundManager.Instance.SwitchToCasket();
    }

    public void StartHardMode()
    {
        SceneManager.LoadScene("Level 4");
        SoundManager.Instance.SwitchToCasket();
    }

    public void StartCredits()
    {
        SceneManager.LoadScene("Credits");
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
