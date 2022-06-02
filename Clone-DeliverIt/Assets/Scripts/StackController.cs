using System.Collections.Generic;
using UnityEngine;

public class StackController : MonoBehaviour
{
    [SerializeField] private Transform _stackHolder;

    private Stack<Cargo> _cargoStack;

    private void OnEnable()
    {
        _cargoStack = new Stack<Cargo>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            interactable.Interact(this);
        }
    }

    public void AddToStack(Cargo cargo)
    {
        Transform cargoTransform = cargo.transform;

        cargoTransform.SetParent(_stackHolder);
        cargoTransform.localPosition = NewCargoPosition(cargo.Height);
        cargoTransform.localRotation = cargoTransform.rotation;

        _cargoStack.Push(cargo);
    }

    public bool RemoveFromStack()
    {
        if (_cargoStack.Count <= 0)
        {
            return false;
        }
        else
        {
            _cargoStack.Pop().gameObject.SetActive(false);
            
            return true;
        }
    }

    private Vector3 NewCargoPosition(float cargoHeight)
    {
        Vector3 position = Vector3.zero;

        if (_cargoStack.Count == 0)
        {
            position.y += cargoHeight / 2;
        }
        else if (_cargoStack.Count > 0)
        {
            position.y = HeightOfTheStack() + cargoHeight / 2;
        }

        return position;
    }

    private float HeightOfTheStack()
    {
        Cargo lastCargo = _cargoStack.Peek();
        return lastCargo.transform.localPosition.y + lastCargo.Height / 2;
    }
}