using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MystiaTrigger : MonoBehaviour {

    public DialogueController dialog;
    public MystiaBehaviour chin;

    public Transform bossCanvas;
    public Transform bossBlocker;

    public FairySpawner[] spawner;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            dialog.EnableDialogue();
            chin.start = true;
            bossBlocker.gameObject.SetActive(true);
            bossCanvas.gameObject.SetActive(true);

            foreach (FairySpawner item in spawner)
                item.start = true;

            this.GetComponent<Collider2D>().enabled = false;
            SoundManager.Instance.SwitchToMagus();
            gameObject.SetActive(false);
        }
    }
}
