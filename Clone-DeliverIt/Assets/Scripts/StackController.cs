using System.Collections.Generic;
using UnityEngine;

public class StackController : MonoBehaviour
{
    [SerializeField] private Transform _stackHolder;

    private List<Cargo> _cargoList;

    private void OnEnable()
    {
        _cargoList = new List<Cargo>();
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

        _cargoList.Add(cargo);
    }

    public bool RemoveFromStack()
    {
        if (_cargoList.Count <= 0)
        {
            return false;
        }
        else
        {
            _cargoList[^1].gameObject.SetActive(false);
            _cargoList.RemoveAt(_cargoList.Count - 1);
            
            return true;
        }
    }

    {

        {
        }
    }

    private float HeightOfTheStack()
    {
        Cargo lastCargo = _cargoList[^1];
        return lastCargo.transform.localPosition.y + lastCargo.Height / 2;
    }
}