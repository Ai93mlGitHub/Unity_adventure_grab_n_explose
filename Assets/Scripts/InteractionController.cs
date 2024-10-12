using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private float _explosionForce = 700f;
    [SerializeField] private ParticleSystem _explosionVFXPrefab;

    [SerializeField] private LayerMask _groundLayerMask;

    private Grabber _grabber;
    private Exploser _exploser;
    private int _leftMouseButton = 0;
    private int _rightMouseButton = 1;

    private void Awake()
    {
        _grabber = new Grabber(_groundLayerMask);
        _exploser = new Exploser(_explosionRadius, _explosionForce, _explosionVFXPrefab);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(_leftMouseButton))
            _grabber.TryGrabObject();

        if (Input.GetMouseButton(_leftMouseButton))
            _grabber.DragObject();

        if (Input.GetMouseButtonUp(_leftMouseButton))
            _grabber.ReleaseObject();

        if (Input.GetMouseButtonDown(_rightMouseButton))
        {
            _exploser.Explose();
            Instantiate(_exploser.ExplosionVFXPrefab, _exploser.ExplosionPosition, Quaternion.identity);
        }
    }
}
