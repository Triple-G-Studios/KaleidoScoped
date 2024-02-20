using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // Outlets
    public GameObject collectiblePrefab;

    //NavMeshAgent navAgent;
    Animator animator;

    // Configuration
    public Transform target;
    //public Transform patrolRoute;

    // State Tracking
    //int patrolIndex;

    // Methods
    void Start()
    {
        //navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            //navAgent.SetDestination(target.position);
        }

        //animator.SetFloat("velocity", navAgent.velocity.magnitude);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Projectile"))
        {
            Destroy(collision.gameObject); // Destroy projectile
            Destroy(gameObject); // Destroy enemy
            Instantiate(collectiblePrefab, transform.position, Quaternion.identity); // Spawn paint collectible
        }
    }
}
