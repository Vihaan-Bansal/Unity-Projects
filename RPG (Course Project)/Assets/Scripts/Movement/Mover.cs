using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {

        [SerializeField] private Transform target;
        private NavMeshAgent navMeshAgent;

        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            //if (Input.GetMouseButton(0))
            //{
            //    MoveToCursor();
            //}

            UpdateAnimator();
        }


        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
        }

        private void UpdateAnimator()
        {
            // Global Velocity
            Vector3 velocity = navMeshAgent.velocity;
            // turns velocity from world space to local space
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);

            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }
    }
}
