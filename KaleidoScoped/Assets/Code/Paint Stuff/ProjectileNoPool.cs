using System.Collections;
using UnityEngine;
using UnityEditor;
using Mirror;

namespace Kaleidoscoped
{
    public class ProjectileNoPool : MonoBehaviour
    {
        [SerializeField] private float lifetime = 5f;
        [SerializeField] private GameObject paintSplatPrefab;
        private Color splatterColor;

        // This method has been simplified to remove the pool parameter
        public void Initialize(Vector3 direction, float force, Color splatColor)
        {
            splatterColor = splatColor;

            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero; // Reset velocity to ensure the force applies correctly
            rb.AddForce(direction * force, ForceMode.Impulse);

            StartCoroutine(DestroyAfterDelay(lifetime));
        }

        private void OnCollisionEnter(Collision collision)
        {
            // Handle collision with non-player and non-enemy objects
            if (!collision.collider.CompareTag("Enemy") && !collision.collider.CompareTag("Player"))
            {
                CreatePaintSplat(collision);
            }

            // Destroy or deactivate the projectile
            Destroy(gameObject); // Or Deactivate if using a future pooling mechanism
        }

        private void CreatePaintSplat(Collision collision)
        {
            ContactPoint contact = collision.contacts[0];
            Quaternion rotation = Quaternion.LookRotation(-contact.normal);
            Vector3 position = contact.point + contact.normal * 0.01f; // Offset to prevent z-fighting

            GameObject splat = Instantiate(paintSplatPrefab, position, rotation);
            Renderer splatRenderer = splat.GetComponent<Renderer>();
            splatRenderer.material.color = splatterColor;

            // If you have a SplatterController script handling specific logic for the splat
            SplatterController splatter = splat.GetComponent<SplatterController>();
            // if (splatter != null)
            // {
            //     // Handle additional splatter setup here, if necessary
            // }
        }

        private IEnumerator DestroyAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            Destroy(gameObject);
        }
    }
}