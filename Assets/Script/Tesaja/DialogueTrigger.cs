using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public string[] dialogueSentences;  // Array dialog
    private DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    public void TriggerDialogue()
    {
        dialogueManager.StartDialogue(dialogueSentences);
    }
}
