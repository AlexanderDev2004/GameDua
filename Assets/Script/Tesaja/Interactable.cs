using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string interactionMessage = "Interacting...";

    public void Interact(System.Action onComplete)
    {
        Debug.Log(interactionMessage);
        Invoke(nameof(CompleteInteraction), 2f); // Simulate interaction duration
        void CompleteInteraction()
        {
            Debug.Log("Interaction complete!");
            onComplete?.Invoke();
        }
    }
}
