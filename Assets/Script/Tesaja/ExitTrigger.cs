using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTrigger : MonoBehaviour
{
    private bool canExit = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canExit = true;
            Debug.Log("Press E to exit school.");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canExit = false;
        }
    }

    void Update()
    {
        if (canExit && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Transitioning to imagination world...");
            SceneManager.LoadScene("ImaginationScene");
        }
    }
}
