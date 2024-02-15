using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifetime = 5f;
    [SerializeField] private GameObject paintSplatPrefab;

    private ProjectilePool pool;

    public void Initialize(ProjectilePool projectilePool, Vector3 direction, float force)
    {
        pool = projectilePool;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.AddForce(direction * force, ForceMode.Impulse);

        StartCoroutine(ReturnToPoolAfterDelay(lifetime));
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Paint splatter
        ContactPoint contact = collision.contacts[0];

        Quaternion rotation = Quaternion.LookRotation(-contact.normal);

        Vector3 position = contact.point + contact.normal * 0.01f;

        Instantiate(paintSplatPrefab, position, rotation);

        //BUG I noticed a bug where you can shoot another paint splatter and it makes like infinite paint splatters on top of eachother. 
        //BUG Since the paint splatter is raised a little bit you can shoot a bunch of them and they stack on top of eachother.
        //BUG If you run and or jump while shooting the player like contacts the projectile and it like applies a force ...weird stuff happens.
        //TODO Need to think about the surfaces that the paint splatters on. Like if it hits payer it shouldn't just make a floating splatter in the air.

        ReturnToPool();
    }

    private IEnumerator ReturnToPoolAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        if (pool != null)
        {
            pool.ReturnProjectile(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
