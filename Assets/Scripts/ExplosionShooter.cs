using UnityEngine;

public class ExplosionShooter : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionParticle;
    [SerializeField] private float _explosionForce = 20f;
    [SerializeField] private float _explosionRadius = 5f;

    bool isMoving = false;

    public void Update()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(cameraRay, out RaycastHit hit))
        {
            Collider hitCollider = hit.collider;

            if (Input.GetMouseButtonDown(1))
            {
                Instantiate(_explosionParticle, hit.point, Quaternion.identity);

                Collider[] explodingItems = Physics.OverlapSphere(hit.point, _explosionRadius);

                foreach (Collider collider in explodingItems)
                {
                    Rigidbody rigidbody = collider.GetComponent<Rigidbody>();

                    if (rigidbody != null)
                    {
                        rigidbody.AddExplosionForce(_explosionForce, hit.point, _explosionRadius);
                    }
                }
            }

            if (Input.GetMouseButton(0))
            {
                if (hitCollider.GetComponent<Rigidbody>() != null)
                {
                    isMoving = true;
                }
            }
            else
            {
                isMoving = false;
            }

            if (isMoving)
            {
                hitCollider.transform.position = hit.point;
            }
        }
    }
}
