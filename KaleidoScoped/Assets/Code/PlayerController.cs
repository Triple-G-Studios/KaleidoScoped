using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    // Outlets
    public Transform povOrigin;
    public Transform projectileOrigin;
    public GameObject projectilePrefab;

    public ProjectilePool projectilePool;

    // Configuration
    public float attackRange;

    // State Tracking
    public List<string> keyIdsObtained;

    void Awake()
    {
        instance = this;
        keyIdsObtained = new List<string>();
    }

    void OnInteract()
    {
        RaycastHit hit;
        if (Physics.Raycast(povOrigin.position, povOrigin.forward, out hit, 2f))
        {
            //Debug: Test first person interactions
            //print("Interacted with " + hit.transform.name + " from " + hit.distance + " m.");

            //Doors
            // Door targetDoor = hit.transform.GetComponent<Door>();
            // if (targetDoor)
            // {
            //     targetDoor.Interact();
            // }

            //Buttons
            // InteractButton targetButton = hit.transform.GetComponent<InteractButton>();
            // if (targetButton != null)
            // {
            //     targetButton.Interact();
            // }
        }
    }

    // I swapped these to make shooting on left click
    void OnSecondaryAttack()
    {
        RaycastHit hit;
        bool hitSomething = Physics.Raycast(povOrigin.position, povOrigin.forward, out hit, attackRange);
        if (hitSomething)
        {
            Rigidbody targetRigidbody = hit.transform.GetComponent<Rigidbody>();
            if (targetRigidbody)
            {
                targetRigidbody.AddForce(povOrigin.forward * 100f, ForceMode.Impulse);
            }
        }
    }
    // I swapped these to make shooting on left click
    void OnPrimaryAttack()
    {
        GameObject projectile = projectilePool.GetProjectile();
        projectile.transform.position = projectileOrigin.position;
        projectile.transform.rotation = Quaternion.LookRotation(povOrigin.forward);

        Projectile projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.Initialize(projectilePool, povOrigin.forward, 25f);
        }
    }



}


