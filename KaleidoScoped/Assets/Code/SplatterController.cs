using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatterController : MonoBehaviour
{
    public static SplatterController instance;
    public string color;

    private void Awake()
    {
        instance = this;
    }

    void OnTriggerEnter(Collider other)
    {
        FirstPersonController targetPlayer = other.GetComponent<FirstPersonController>();

        if (targetPlayer != null)
        {
            print("with player");
            //if it has a first person controller, it has a playercontroller
            color = other.GetComponent<PlayerController>().currentColor;

            if (color == "purple")
            {
                targetPlayer.JumpHeight *= 2;
            }

            else if (color == "blue")
            {
                targetPlayer.MoveSpeed *= 2;
                targetPlayer.SprintSpeed *= 2;
            }

            else if (color == "green")
            {
                targetPlayer.MoveSpeed /= 2;
                targetPlayer.SprintSpeed /= 2;
            }

            else if (color == "red")
            {
                targetPlayer.JumpHeight = 0;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        FirstPersonController targetPlayer = other.GetComponent<FirstPersonController>();

        if (targetPlayer != null)
        {
            //if it has a first person controller, it has a playercontroller
            color = other.GetComponent<PlayerController>().currentColor;

            if (color == "purple")
            {
                targetPlayer.JumpHeight /= 2;
            }

            else if (color == "blue")
            {
                targetPlayer.MoveSpeed /= 2;
                targetPlayer.SprintSpeed /= 2;
            }

            else if (color == "green")
            {
                targetPlayer.MoveSpeed *= 2;
                targetPlayer.SprintSpeed *= 2;
            }

            else if (color == "red")
            {
                targetPlayer.JumpHeight = 1.2f;
            }
        }
    }
}
