using System.Collections.Generic;
using UnityEngine;

public class StackController : MonoBehaviour
{
    [SerializeField] private Transform _stackHolder;
    [SerializeField] private GameObject _moneyPrefab;

    private int _cargoCountOnStack;
    private int _moneyCountOnStack;

    private List<Cargo> _cargoList;

    public int CargoCountOnStack => _cargoCountOnStack;

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
        cargo.SetNewPositionOfCargo(_stackHolder, GetHeightOfTheCargo(_cargoList.Count - 1));

        _cargoList.Add(cargo);
        _cargoCountOnStack++;
    }

    public Cargo DeliverCargo()
    {
        if (_cargoList.Count > 0)
        {
            Cargo cargo = _cargoList[_moneyCountOnStack];
            _cargoCountOnStack--;

            ClaimMoney();
            UpdateCargoBoxesPositions();

            return cargo;
        }

        return null;
    }

    private void ClaimMoney()
    {
        Cargo moneyCargo = Instantiate(_moneyPrefab).GetComponent<Cargo>();

        moneyCargo.SetNewPositionOfCargo(_stackHolder, GetHeightOfTheCargo(_moneyCountOnStack - 1));

        _cargoList[_moneyCountOnStack] = moneyCargo;
        _moneyCountOnStack++;
    }

    private void UpdateCargoBoxesPositions()
    {
        for (int i = _moneyCountOnStack; i < _cargoList.Count; i++)
        {
            _cargoList[i].SetNewPositionOfCargo(_stackHolder, GetHeightOfTheCargo(i - 1));
        }
    }

    private float GetHeightOfTheCargo(int index)
    {
        if (_cargoList.Count == 0 || index < 0)
        {
            return 0;
        }

        Cargo cargo = _cargoList[index];
        return cargo.transform.localPosition.y + cargo.Height / 2;
    }
}