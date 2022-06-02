using UnityEngine;

public class Customer : MonoBehaviour, IInteractable
{
    [SerializeField] private int _expectedCargo;

    private bool _isUsed;

    public void Interact(PlayerController playerController)
    {
    }

    public void Interact(StackController stackController)
    {
        if (_isUsed) return;
        _isUsed = true;

        for (int i = 0; i < _expectedCargo; i++)
        {
            if (stackController.RemoveFromStack())
            {
                
            }
            else
            {
                break;
            }
        }
    }
}