using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintMovement : MonoBehaviour
{
    public string color = "blue";

    void OnTriggerEnter(Collider other)
    {
        FirstPersonController targetPlayer = other.GetComponent<FirstPersonController>();

        if (targetPlayer != null)
        {
            if (color == "purple")
            {
                targetPlayer.JumpHeight *= 2;
            }

            if (color == "blue")
            {
                targetPlayer.MoveSpeed *= 2;
                targetPlayer.SprintSpeed *= 2;
            }

            if (color == "green")
            {
                targetPlayer.MoveSpeed /= 2;
                targetPlayer.SprintSpeed /= 2;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        FirstPersonController targetPlayer = other.GetComponent<FirstPersonController>();

        if (targetPlayer != null)
        {
            if (color == "purple")
            {
                targetPlayer.JumpHeight /= 2;
            }

            if (color == "blue")
            {
                targetPlayer.MoveSpeed /= 2;
                targetPlayer.SprintSpeed /= 2;
            }

            if (color == "green")
            {
                targetPlayer.MoveSpeed *= 2;
                targetPlayer.SprintSpeed *= 2;
            }
        }
    }
}
