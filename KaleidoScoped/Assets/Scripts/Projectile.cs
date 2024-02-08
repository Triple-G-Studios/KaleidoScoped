using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintballProjectile : MonoBehaviour
{
    [SerializeField] private float lifetime = 5f;
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
        // Handle collision 
        //If(hit player) => damage them
        //If(hit terrain) => create a paint splat

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
