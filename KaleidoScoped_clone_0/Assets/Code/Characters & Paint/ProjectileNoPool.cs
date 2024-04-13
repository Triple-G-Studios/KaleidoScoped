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
        private string cString;

        public float damage = 100f;

        [SerializeField] private GameObject shooter;


        public void Initialize(Vector3 direction, float force, Color splatColor, string colorString, GameObject shooterGameObject)
        {
            splatterColor = splatColor;
            cString = colorString;
            shooter = shooterGameObject;


            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero; // Reset velocity to ensure the force applies correctly
            rb.AddForce(direction * force, ForceMode.Impulse);

            StartCoroutine(DestroyAfterDelay(lifetime));
        }

        private void OnCollisionEnter(Collision collision)
        {
            GameObject hitObject = collision.gameObject;

            // Check if the hit object or any of its parents is the shooter
            bool hitShooter = false;
            while (hitObject != null)
            {
                if (hitObject == shooter)
                {
                    hitShooter = true;
                    break;
                }
                if (hitObject.transform.parent != null)
                {
                    hitObject = hitObject.transform.parent.gameObject;
                }
                else
                {
                    break;
                }
            }

            if (hitShooter)
            {
                Debug.Log("Projectile hit the shooter. Ignoring.");
                return;
            }

            if (!collision.collider.CompareTag("Enemy") && !collision.collider.CompareTag("Player"))
            {
                CreatePaintSplat(collision);
            }

            var hitPlayer = collision.collider.GetComponentInParent<PlayerHealth>(); // Assuming you have a PlayerHealth script

            CharacterSelection characterSelection = GetComponent<CharacterSelection>();
            if (characterSelection != null)
            {
                // do nothing
            }
            else
            {
                Debug.LogError("CharacterSelection component not found on player object.");
            }

            if (hitPlayer != null && NetworkServer.active)
            {
                hitPlayer.TakeDamage(damage);
                Destroy(gameObject);
            } else
            {
                Debug.Log("Enemy player is null or server is inactive");
            }

            Destroy(gameObject);
        }

        private void CreatePaintSplat(Collision collision)
        {
            SFXManager.instance.PlaySoundSplat();
            ContactPoint contact = collision.contacts[0];
            Quaternion rotation = Quaternion.LookRotation(-contact.normal);
            Vector3 position = contact.point + contact.normal * 0.01f; // Offset to prevent z-fighting

            GameObject splat = Instantiate(paintSplatPrefab, position, rotation);
            Renderer splatRenderer = splat.GetComponent<Renderer>();
            splatRenderer.material.color = splatterColor;

            SplatterController splatter = splat.GetComponent<SplatterController>();
            if (splatter != null) splatter.color = cString;

        }

        private IEnumerator DestroyAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            Destroy(gameObject);
        }
    }
}