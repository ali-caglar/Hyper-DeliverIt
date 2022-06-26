using UnityEngine;
using Lean.Touch;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private TrailRenderer _skidMarkTrail;
    [SerializeField] private ParticleSystem _smokeParticle;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        LeanTouch.OnFingerOld += OnTapHandler;
        LeanTouch.OnFingerUp += OnFingerUpHandler;
    }

    private void OnDisable()
    {
        LeanTouch.OnFingerOld -= OnTapHandler;
        LeanTouch.OnFingerUp -= OnFingerUpHandler;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            interactable.Interact(this);
        }
    }

    private void OnTapHandler(LeanFinger finger)
    {
        _smokeParticle.Play();
        _skidMarkTrail.emitting = false;
    }

    private void OnFingerUpHandler(LeanFinger obj)
    {
        _smokeParticle.Stop();
        _skidMarkTrail.emitting = true;
    }
}