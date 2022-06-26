using UnityEngine;
using TMPro;

public class Customer : MonoBehaviour, IInteractable
{
    [SerializeField] private int _expectedCargoCount;
    [SerializeField] private Transform _cargoDeliverSpot;

    [SerializeField] private TextMeshProUGUI _cargoCountTMP;

    private bool _isUsed;
    private float _heightOfTheStack;

    private void Start()
    {
        _cargoCountTMP.text = $"x {_expectedCargoCount}";
    }

    public void Interact(PlayerController playerController)
    {
    }

    public void Interact(StackController stackController)
    {
        if (_isUsed) return;
        _isUsed = true;

        for (int i = 0; i < _expectedCargoCount; i++)
        {
            if (stackController.CargoCountOnStack > 0)
            {
                Cargo cargo = stackController.DeliverCargo();
                if (cargo == null) return;

                cargo.SetNewPositionOfCargo(_cargoDeliverSpot, _heightOfTheStack);
                _heightOfTheStack = cargo.transform.localPosition.y + cargo.Height / 2;
            }
            else
            {
                break;
            }
        }
    }
}