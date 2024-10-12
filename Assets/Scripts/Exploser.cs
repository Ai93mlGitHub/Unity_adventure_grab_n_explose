using UnityEngine;

public class Exploser
{
    private float _explosionRadius = 5f;
    private float _explosionForce = 700f;

    public Vector3 ExplosionPosition { get; private set; } = new Vector3();
    public ParticleSystem ExplosionVFXPrefab { get; private set; }

    public Exploser() { }

    public Exploser(float explosionRadius, float explosionForce, ParticleSystem explosionVFXPrefab)
    {
        _explosionRadius = explosionRadius;
        _explosionForce = explosionForce;
        ExplosionVFXPrefab = explosionVFXPrefab;
    }

    public void Explose()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            ExplosionPosition = hit.point;
            Collider[] colliders = Physics.OverlapSphere(ExplosionPosition, _explosionRadius);

            foreach (Collider nearbyObject in colliders)
            {
                Rigidbody rigidBody = nearbyObject.GetComponent<Rigidbody>();

                if (rigidBody != null)
                    rigidBody.AddExplosionForce(_explosionForce, ExplosionPosition, _explosionRadius);
            }
        }
    }
}
