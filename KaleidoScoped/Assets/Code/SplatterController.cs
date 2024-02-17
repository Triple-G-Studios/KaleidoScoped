using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintMovement : MonoBehaviour
{
    string color;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController targetPlayer = other.GetComponent<PlayerController>();

        if (targetPlayer != null)
        {
            if (color == "purple")
            {

            }
        }
    }
}
