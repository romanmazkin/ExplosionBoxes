using UnityEngine;

public class ExplosionLauncher : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionParticle;
    [SerializeField] private float _explosionForce = 500f;
    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] LayerMask _mask;

    public void MakeExplosionIn(RaycastHit hit)
    {
        Instantiate(_explosionParticle, hit.point, Quaternion.identity);

        Collider[] explodingItems = Physics.OverlapSphere(hit.point, _explosionRadius, _mask.value);

        foreach (Collider collider in explodingItems)
        {
            Rigidbody rigidbody = collider.GetComponent<Rigidbody>();

            if (rigidbody != null)
                rigidbody.AddExplosionForce(_explosionForce, hit.point, _explosionRadius);
        }
    }
}
