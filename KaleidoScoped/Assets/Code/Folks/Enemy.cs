using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Kaleidoscoped
{
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

        KillCounter killCounter;

        // Methods
        private void Start()
        {
            //navAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            killCounter = GameObject.FindWithTag("KillCounter").GetComponent<KillCounter>();
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
                killCounter.IncrementKills(); // Update kills
                Instantiate(collectiblePrefab, transform.position, Quaternion.identity); // Spawn paint collectible
            }
        }
    }
}