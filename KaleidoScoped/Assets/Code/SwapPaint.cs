using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaleidoscoped
{
    public class SwapPaint : MonoBehaviour
    {
        // Outlets
        public GameObject splatterPrefab;
        public GameObject paintballPrefab;

        // Configuration
        public string color;

        // Methods
        void OnTriggerEnter(Collider other)
        {
            PlayerController targetPlayer = other.GetComponent<PlayerController>();
            if (targetPlayer != null)
            {
                Color paintColor = Color.red; // Default
                switch (color.ToLower())
                {
                    case "purple":
                        paintColor = new Color(1f, 0f, 1f);
                        targetPlayer.currentColor = "purple";
                        break;
                    case "red":
                        paintColor = Color.red;
                        targetPlayer.currentColor = "red";
                        break;
                    case "green":
                        paintColor = Color.green;
                        targetPlayer.currentColor = "green";
                        break;
                    case "blue":
                        paintColor = Color.blue;
                        targetPlayer.currentColor = "blue";
                        break;
                }

                targetPlayer.color = paintColor;
                //var splatterRenderer = splatterPrefab.GetComponent<Renderer>();
                var paintballRenderer = paintballPrefab.GetComponent<Renderer>();
                //splatterRenderer.sharedMaterial.SetColor("_Color", paintColor); // Set splatter color
                paintballRenderer.sharedMaterial.SetColor("_Color", paintColor); // Set paintball color

                Destroy(gameObject);
            }
        }
    }
}
