using UnityEngine;

public class Box : MonoBehaviour, IGrabbable
{
    [SerializeField] private float _scaleOnGrab = 1.5f;

    private Vector3 _originalScale;

    private void Awake() => _originalScale = transform.localScale;

    public void OnGrab() => transform.localScale *= _scaleOnGrab;

    public void OnRelease() => transform.localScale = _originalScale;
}
