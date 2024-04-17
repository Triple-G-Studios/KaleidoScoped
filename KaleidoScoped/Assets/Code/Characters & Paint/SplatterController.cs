using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaleidoscoped
{
    public class SplatterController : MonoBehaviour
    {
        public static SplatterController instance;
        public string color;

        private void Awake()
        {
            instance = this;
        }

        void OnTriggerStay(Collider other)
        {
            LukePlayerMovement targetPlayer = other.GetComponent<LukePlayerMovement>();

            if (targetPlayer != null)
            {
                //if it has a first person controller, it has a playercontroller
                //color = other.GetComponent<PlayerController>().currentColor;

                if (color == "purple")
                {
                    targetPlayer.JumpHeight = 2.4f;
                }

                else if (color == "blue")
                {
                    targetPlayer.MoveSpeed = 8f;
                    targetPlayer.SprintSpeed = 10f;
                }

                else if (color == "green")
                {
                    targetPlayer.MoveSpeed = 2f;
                    targetPlayer.SprintSpeed = 2f;
                }

                else if (color == "red")
                {
                    targetPlayer.JumpHeight = 0.1f;
                }

                else
                {
                    targetPlayer.MoveSpeed = 4f;
                    targetPlayer.SprintSpeed = 6f;
                    targetPlayer.JumpHeight = 1.2f;
                }
            }
        }

        void OnTriggerExit(Collider other)
        {
            LukePlayerMovement targetPlayer = other.GetComponent<LukePlayerMovement>();

            if (targetPlayer != null)
            {
                //if it has a first person controller, it has a playercontroller

                targetPlayer.MoveSpeed = 4f;
                targetPlayer.SprintSpeed = 6f;
                targetPlayer.JumpHeight = 1.2f;
            }
        }
    }
}
