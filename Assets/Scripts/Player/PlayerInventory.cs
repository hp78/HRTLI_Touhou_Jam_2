using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{

    [Header ("Shot List")]
    public List<BulletPattern> shotList;     // queue containing all shots
    public List<BulletPattern> bombList;

    [Header("UI Elements")]
    public Image[] nextShots;
    public Image[] nextBombs;

    Color visible;
    Color notVisible;

    //
    void Start()
    {
        visible = new Color(1, 1, 1, 0.5f);
        notVisible = new Color(1, 1, 1, 0);
    }

    //
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PickUps") && this.GetComponent<PlayerController>().invulFrames < 0.0f)
        {
            shotList.Add(collision.GetComponent<BulletDrop>().Collected());
            UpdateNextShots();
            UpdateNextBombs();
        }
    }

    public void UpdateNextShots()
    {
        if (shotList.Count < 1)
        {
            nextShots[0].color = notVisible;
            return;
        }

        int i = 0;
        foreach(Image image in nextShots)
        {
            if (i < shotList.Count && shotList[i] != null)
            {
                nextShots[i].sprite = shotList[i].shotIcon;
                nextShots[i].color = visible;
            }
            else
            {
                //nextShots[i].sprite = null;
                nextShots[i].color = notVisible;
            }

            ++i;
        }
    }

    public void UpdateNextBombs()
    {
        if (bombList.Count < 1)
        {
            nextBombs[0].color = notVisible;
            return;
        }

        int i = 0;
        foreach (Image image in nextBombs)
        {
            if (i < bombList.Count && bombList[i] != null)
            {
                nextBombs[i].sprite = bombList[i].shotIcon;
                nextBombs[i].color = visible;
            }
            else
            {
                //nextBombs[i].sprite = null;
                nextBombs[i].color = notVisible;
            }

            ++i;
        }
    }
}
