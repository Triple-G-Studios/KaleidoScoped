using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS
{
    public class Enemy : MonoBehaviour
    {
        //Outlets
        UnityEngine.AI.NavMeshAgent navAgent;
        Animator animator;

        //Configuration
        public Transform priorityTarget;
        public Transform target;
        public Transform patrolRoute;

        //State Tracking
        int patrolIndex;
        public float chaseDistance;

        //Methods
        void Start()
        {
            navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            if (patrolRoute)
            {
                //Which patrol point is active?
                target = patrolRoute.GetChild(patrolIndex);

                //How far is the patrol point?
                float distance = Vector3.Distance(transform.position, target.position);
                print("Distance: " + distance); //DEBUG distance so we can configure a threshold

                //Target the next point we are close enough
                if (distance < 1.6f)
                {
                    patrolIndex++;
                    if (patrolIndex >= patrolRoute.childCount)
                    {
                        patrolIndex = 0;
                    }
                }
            }

            if (priorityTarget)
            {
                //Keep track of our priority target
                float priorityTargetDistance = Vector3.Distance(transform.position, priorityTarget.position);

                //If the priority target gets too close, follow it and highlight ourselves
                if (priorityTargetDistance < chaseDistance)
                {
                    target = priorityTarget;
                    // GetComponent<Renderer>().material.color = Color.red;
                }
                else
                {
                    // GetComponent<Renderer>().material.color = Color.white;
                }
            }

            if (target)
            {
                navAgent.SetDestination(target.position);
            }

            animator.SetFloat("velocity", navAgent.velocity.magnitude);
        }

    }

}
