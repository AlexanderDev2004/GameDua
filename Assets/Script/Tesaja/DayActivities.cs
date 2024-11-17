using UnityEngine;
using UnityEngine.SceneManagement;

public class DayActivitiesManager : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float movementSpeed = 5f; // Player's movement speed
    public LayerMask interactableLayer; // Layer for interactable objects
    public string nightSceneName = "NightActivities"; // Name of the night scene

    private bool isInteracting = false; // Flag to check if the player is interacting
    private bool canExit = false; // Flag to check if the player can exit the scene

    void Update()
    {
        HandleMovement();
        HandleInteraction();
    }

    // Handles player movement
    private void HandleMovement()
    {
        if (isInteracting) return;

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical) * movementSpeed * Time.deltaTime;
        player.Translate(movement);
    }

    // Handles interaction with objects and NPCs
    private void HandleInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Press 'E' to interact
        {
            RaycastHit hit;
            if (Physics.Raycast(player.position, player.forward, out hit, 2f, interactableLayer))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    isInteracting = true;
                    interactable.Interact(OnInteractionComplete);
                }
            }
        }

        if (canExit && Input.GetKeyDown(KeyCode.F)) // Press 'F' to exit
        {
            TransitionToNightScene();
        }
    }

    // Callback when interaction is complete
    private void OnInteractionComplete()
    {
        isInteracting = false;
    }

    // Enables the exit trigger
    public void EnableExit()
    {
        canExit = true;
        Debug.Log("You can now exit to the night scene.");
    }

    // Transitions to the night scene
    private void TransitionToNightScene()
    {
        Debug.Log("Transitioning to the night scene...");
        SceneManager.LoadScene(nightSceneName);
    }
}
