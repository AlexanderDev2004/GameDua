using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;  
    public GameObject dialogueBox;  
    public float typingSpeed = 0.05f;

    private System.Collections.Generic.Queue<string> sentences;  
    private bool isTyping = false;   

    void Start()
    {
        sentences = new System.Collections.Generic.Queue<string>();
        dialogueBox.SetActive(false);
    }

    public void StartDialogue(string[] dialogues)
    {
        dialogueBox.SetActive(true);
        sentences.Clear();

        foreach (string sentence in dialogues)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (isTyping) return;

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    void EndDialogue()
    {
        dialogueBox.SetActive(false);
    }
}
