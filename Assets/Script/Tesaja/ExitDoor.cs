using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    public DayActivitiesManager activitiesManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            activitiesManager.EnableExit();
        }
    }
}
