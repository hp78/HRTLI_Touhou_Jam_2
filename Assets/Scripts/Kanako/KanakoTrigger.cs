using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KanakoTrigger : MonoBehaviour {


    public DialogueController dialog;
    public KanakoBehaviour baba;
    public Transform bossBlocker;
    public Transform bossCanvas;

    public FairySpawner[] spawner;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            dialog.EnableDialogue();
            baba.start = true;
            bossBlocker.gameObject.SetActive(true);
            bossCanvas.gameObject.SetActive(true);

            foreach (FairySpawner item in spawner)
                item.start = true;

            gameObject.SetActive(false);
            SoundManager.Instance.SwitchToMagus();
            this.GetComponent<Collider2D>().enabled = false;

        }
    }
}
