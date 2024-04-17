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
        public Color tempColor;

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
                Renderer renderer = this.GetComponent<Renderer>();
                tempColor = renderer.material.color;
                if (tempColor == Color.magenta)
                {
                    color = "purple";
                } else if (tempColor == Color.blue)
                {
                    color = "blue";
                }
                else if (tempColor == Color.green)
                {
                    color = "green";
                }
                else if (tempColor == Color.red)
                {
                    color = "red";
                }
                if (color == "purple" || color == "magenta")
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
