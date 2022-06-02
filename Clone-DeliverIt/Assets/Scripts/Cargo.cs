using UnityEngine;

public class Cargo : MonoBehaviour, IInteractable
{
    [SerializeField] private float _height;

    public float Height => _height;
    
    private bool _isUsed;
    
    public void Interact(PlayerController player)
    {
        
    }

    public void Interact(StackController stackController)
    {
        if (_isUsed) return;
        _isUsed = true;
        
        stackController.AddToStack(this);
    }
}