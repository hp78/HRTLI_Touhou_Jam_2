using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour {

    public BoolVariable bvGamePaused;
    public BoolVariable bvGameRunning;


    public DialogueSet[] sets;
    int currSet = 0;
    bool isSetActive = false;

    [Header("Left Panel")]
    public Transform left;
    public Image leftBack;
    public Image leftBackPortrait;
    public Image leftTopPortrait;
    public Image leftPortrait;
    public Image leftEmote;
    public Text leftName;
    public Text leftDialogue;
    
    [Header("Right Panel")]
    public Transform right;
    public Image rightBack;
    public Image rightBackPortrait;
    public Image rightTopPortrait;
    public Image rightPortrait;
    public Image rightEmote;
    public Text rightName;
    public Text rightDialogue;

    [Header("Character Sprites")]
    public Sprite marisa;
    public Sprite chen;
    public Sprite cirno;
    public Sprite mystia;
    public Sprite kanako;
    public Sprite okuu;
    public Sprite miko;

    [Header("Character Color")]
    public Color marisaColor;
    public Color chenColor;
    public Color cirnoColor;
    public Color mystiaColor;
    public Color kanakoColor;
    public Color okuuColor;
    public Color mikoColor;
    public Color greyColor;

    [Header("Character Sprites")]
    public Sprite shock;
    public Sprite confused;
    public Sprite sweat;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void LateUpdate () {

		if(isSetActive && !bvGamePaused.data)
        {
            if(Input.GetKeyDown(KeyCode.Z))
            {
                NextDialogue();
            }
        }
	}

    public void NextDialogue()
    {
        if(sets[currSet].NextDialogue())
        {
            DisableDialogue();
        }
        else
        {
            UpdateDialogue();
        }
    }

    public void EnableDialogue()
    {
        if (currSet == sets.Length) return;

        isSetActive = true;
        bvGameRunning.data = false;
        UpdateDialogue();
    }

    void DisableDialogue()
    {
        left.gameObject.SetActive(false);
        right.gameObject.SetActive(false);
        isSetActive = false;
        bvGameRunning.data = true;

        ++currSet;
    }

    public void UpdateDialogue()
    {
        //
        DialogueSet thisSet = sets[currSet];
        bool isLeft = thisSet.set[thisSet.currSet].isLeftChar;
        //
        if (isLeft)
        {
            //
            left.gameObject.SetActive(true);
            left.SetAsLastSibling();

            //
            leftBackPortrait.color = SetCharColor(thisSet.set[thisSet.currSet].character);
            leftPortrait.sprite = SetCharPortrait(thisSet.set[thisSet.currSet].character);
            leftEmote.sprite = SetEmote(thisSet.set[thisSet.currSet].emote);
            leftName.text = SetCharName(thisSet.set[thisSet.currSet].character);
            leftDialogue.text = thisSet.set[thisSet.currSet].sentence;

            leftBack.color = Color.white;
            leftTopPortrait.color = Color.white;
            leftPortrait.color = Color.white;
            leftEmote.color = Color.white;

            rightBack.color = greyColor;
            rightTopPortrait.color = greyColor;
            rightBackPortrait.color = greyColor;
            rightPortrait.color = greyColor;
            rightEmote.color = greyColor;
        }
        else
        {
            //
            right.gameObject.SetActive(true);
            right.SetAsLastSibling();

            //
            rightBackPortrait.color = SetCharColor(thisSet.set[thisSet.currSet].character);
            rightPortrait.sprite = SetCharPortrait(thisSet.set[thisSet.currSet].character);
            rightEmote.sprite = SetEmote(thisSet.set[thisSet.currSet].emote);
            rightName.text = SetCharName(thisSet.set[thisSet.currSet].character);
            rightDialogue.text = thisSet.set[thisSet.currSet].sentence;

            rightBack.color = Color.white;
            rightTopPortrait.color = Color.white;
            rightPortrait.color = Color.white;
            rightEmote.color = Color.white;

            leftBack.color = greyColor;
            leftTopPortrait.color = greyColor;
            leftBackPortrait.color = greyColor;
            leftPortrait.color = greyColor;
            leftEmote.color = greyColor;
        }
    }

    public Color SetCharColor(Dialogue.DialogueChar character)
    {
        switch (character)
        {
            case Dialogue.DialogueChar.MARISA:
                return marisaColor;

            case Dialogue.DialogueChar.CHEN:
                return chenColor;

            case Dialogue.DialogueChar.CIRNO:
                return cirnoColor;

            case Dialogue.DialogueChar.MYSTIA:
                return mystiaColor;

            case Dialogue.DialogueChar.KANAKO:
                return kanakoColor;

            case Dialogue.DialogueChar.OKUU:
                return okuuColor;

            case Dialogue.DialogueChar.MIKO:
                return mikoColor;

            default:
                return marisaColor;
        }
    }

    public Sprite SetCharPortrait(Dialogue.DialogueChar character)
    {
        switch (character)
        {
            case Dialogue.DialogueChar.MARISA:
                return marisa;

            case Dialogue.DialogueChar.CHEN:
                return chen;

            case Dialogue.DialogueChar.CIRNO:
                return cirno;

            case Dialogue.DialogueChar.MYSTIA:
                return mystia;

            case Dialogue.DialogueChar.KANAKO:
                return kanako;

            case Dialogue.DialogueChar.OKUU:
                return okuu;

            case Dialogue.DialogueChar.MIKO:
                return miko;

            default:
                return null;
        }
    }

    public string SetCharName(Dialogue.DialogueChar character)
    {
        switch (character)
        {
            case Dialogue.DialogueChar.MARISA:
                return "Marisa";

            case Dialogue.DialogueChar.CHEN:
                return "Chen";

            case Dialogue.DialogueChar.CIRNO:
                return "Cirno";

            case Dialogue.DialogueChar.MYSTIA:
                return "Mystia";

            case Dialogue.DialogueChar.KANAKO:
                return "Kanako";

            case Dialogue.DialogueChar.OKUU:
                return "Utsuho";

            case Dialogue.DialogueChar.MIKO:
                return "Miko";

            default:
                return "";
        }
    }

    public Sprite SetEmote(Dialogue.DialogueEmote emote)
    {
        switch(emote)
        {
            case Dialogue.DialogueEmote.SHOCK:
                return shock;

            case Dialogue.DialogueEmote.CONFUSED:
                return confused;

            case Dialogue.DialogueEmote.SWEAT:
                return sweat;

            default:
                return null;
        }
    }
}
