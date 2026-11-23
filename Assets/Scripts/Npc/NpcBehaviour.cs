using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class NpcBehaviour : MonoBehaviour
{
    [Header("Status")]
    public bool isPlayerClose;
    public bool startEddyGame;
    public bool wasCalled;

    [Header("Dialogue")]
    public NpcDialogue dialogueData;
    public GameObject dialogueCanvas;
    public TextMeshProUGUI dialogueText, nameText;

    private int dialogueIndex;
    private bool isTyping, isDialogueActive;

    void OnEnable()
    {
        if (PlayerPrefs.GetInt($"wasCalled{gameObject.GetInstanceID()}") == 2) wasCalled = true;
        else if (PlayerPrefs.GetInt($"wasCalled{gameObject.GetInstanceID()}") == 1) wasCalled = false;

        if (PlayerPrefs.GetInt($"startEddyGame{gameObject.GetInstanceID()}") == 2) startEddyGame = true;
        else if (PlayerPrefs.GetInt($"startEddyGame{gameObject.GetInstanceID()}") == 1) startEddyGame = false;

        if (wasCalled)
        {
            wasCalled = false;
            StartDialogue();
        }
    }

    void OnDisable()
    {
        PlayerPrefs.SetInt($"startEddyGame{gameObject.GetInstanceID()}", startEddyGame ? 2 : 1);
        PlayerPrefs.SetInt($"wasCalled{gameObject.GetInstanceID()}", wasCalled ? 2 : 1);
    }


    public bool CanInteract()
    {
        return !isDialogueActive;
    }

    public void Interact()
    {
        if (dialogueData == null)
            return;

        if (isDialogueActive)
        {
            NextLine();
        }
        else
        {
            StartDialogue();
        }
    }

    void StartDialogue()
    {
        if (startEddyGame)
        {
            startEddyGame = false;
            wasCalled = true;
            FindFirstObjectByType<TransitionManager>().ChangeToSceneEddy();
            return;
        }


        isDialogueActive = true;
        dialogueIndex = 0;

        nameText.text = dialogueData.npcName;

        dialogueCanvas.SetActive(true);

        StartCoroutine(TypeLine());
    }

    void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.text = dialogueData.dialoguesLines[dialogueIndex];
            isTyping = false;
        }
        else if (++dialogueIndex < dialogueData.dialoguesLines.Length)
        {
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in dialogueData.dialoguesLines[dialogueIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }

        isTyping = false;

        if (dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
            NextLine();
        }
    }

    public void EndDialogue()
    {
        StopAllCoroutines();
        isDialogueActive = false;
        dialogueText.text = "";
        dialogueCanvas.SetActive(false);
    }
}