using UnityEngine;

public class Cargo : MonoBehaviour, IInteractable
{
    [SerializeField] private float _height;

    private bool _isUsed;

    private Collider _collider;
    private Transform _transform;
    private Rigidbody _rigidbody;

    public float Height => _height;

    private void Awake()
    {
        _transform = transform;
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Interact(PlayerController player)
    {
    }

    public void Interact(StackController stackController)
    {
        if (_isUsed) return;
        _isUsed = true;

        stackController.AddToStack(this);
    }

    public void SetNewPositionOfCargo(Transform newParent, float heightOfTheStack)
    {
        _transform.SetParent(newParent);
        _transform.localPosition = new Vector3(0, heightOfTheStack + _height / 2, 0);
    }

    public void HandleCrash(int orderOnTheStack)
    {
        Collider playerCollider = FindObjectOfType<PlayerController>().GetComponent<Collider>();
        Physics.IgnoreCollision(playerCollider, _collider);

        _collider.isTrigger = false;
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce((orderOnTheStack + 1) * 2 * _transform.forward, ForceMode.Impulse);
        _transform.SetParent(null);
    }
}