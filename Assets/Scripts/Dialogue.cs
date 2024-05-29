using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
   [SerializeField] private GameObject dialoguePanel;
   [SerializeField] private TextMeshPro dialogueText;
   [SerializeField,TextArea(6,8)] private string[] dialogueLines;

    private bool isPlayerInRange;
    private bool dialogueStart;
    private int lineIndex;
    private float numero = 0.05f;


    private void Update()
    {
        if (isPlayerInRange == true && Input.GetButton("Fire1"))
        {
            if (!dialogueStart)
            {
               StartDialogue();
            }
            else if (dialogueText.text ==dialogueLines[lineIndex])
            {
                NextDialogueLine();
            }
        }
    }


    private void StartDialogue ()
    {
        dialogueStart = true;
        dialoguePanel.SetActive(true);
        lineIndex = 0;
        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine ()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            dialogueStart = false;
            dialoguePanel.SetActive(false);
        }
    }

    private IEnumerator ShowLine ()
    {
        dialogueText.text = string.Empty;

        foreach (char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSeconds(numero);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }

    }

    private void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
