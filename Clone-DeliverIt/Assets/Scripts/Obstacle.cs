using UnityEngine;

public class Obstacle : MonoBehaviour, IInteractable
{
    public void Interact(PlayerController player)
    {
        
    }

    public void Interact(StackController stackController)
    {
        stackController.Crash();
        gameObject.SetActive(false); // Temp
    }
}