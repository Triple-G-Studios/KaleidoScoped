using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//using Mirror;

namespace Kaleidoscoped
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController instance;

        // Outlets
        public Transform povOrigin;
        public Transform projectileOrigin;
        public GameObject projectilePrefab;

        public ProjectilePool projectilePool;

        // Configuration
        public float attackRange;

        // State Tracking
        public List<string> keyIdsObtained;
        public string currentColor = "";
        public Color color = Color.blue;

        public GameObject pauseMenuUI;

        private bool isPaused = false;

        public InputActionAsset actionAsset;

        private InputActionMap playerActionMap;

        public GameObject playerCamera;
        public GameObject PlayerFollowCamera;
        public GameObject canvas;
        public GameObject CameraRoot;


        void Awake()
        {
            //if (!isLocalPlayer)
            //{
            //    return;
            //}
            instance = this;
            keyIdsObtained = new List<string>();
            playerActionMap = actionAsset.FindActionMap("Player", true);
            pauseMenuUI.SetActive(false);
        }

        //void Start()
        //{
        //    playerActionMap.Enable();
        //    Cursor.lockState = CursorLockMode.Locked;
        //    Cursor.visible = false;

        //    if (!isLocalPlayer)
        //    {
        //        playerCamera.SetActive(false);
        //        PlayerFollowCamera.SetActive(true);
        //        canvas.SetActive(false);
        //        CameraRoot.SetActive(false);
        //    }
        //}


        void Update()
        {
            // GameObject popup = PopUpController.GetComponent<popupmenu>(); 
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                if (isPaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }

        public void PauseGame()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;

            playerActionMap.Disable();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void ResumeGame()
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;

            playerActionMap.Enable();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void OnInteract()
        {
            RaycastHit hit;
            if (Physics.Raycast(povOrigin.position, povOrigin.forward, out hit, 2f))
            {
                //Debug: Test first person interactions
                //print("Interacted with " + hit.transform.name + " from " + hit.distance + " m.");

                //Doors
                // Door targetDoor = hit.transform.GetComponent<Door>();
                // if (targetDoor)
                // {
                //     targetDoor.Interact();
                // }

                //Buttons
                // InteractButton targetButton = hit.transform.GetComponent<InteractButton>();
                // if (targetButton != null)
                // {
                //     targetButton.Interact();
                // }
            }
        }

        // I swapped these to make shooting on left click
        /*void OnSecondaryAttack()
        {
            RaycastHit hit;
            bool hitSomething = Physics.Raycast(povOrigin.position, povOrigin.forward, out hit, attackRange);
            if (hitSomething)
            {
                Rigidbody targetRigidbody = hit.transform.GetComponent<Rigidbody>();
                if (targetRigidbody)
                {
                    targetRigidbody.AddForce(povOrigin.forward * 100f, ForceMode.Impulse);
                }
            }
        }*/
        // I swapped these to make shooting on left click
        void OnPrimaryAttack()
        {
            GameObject projectile = projectilePool.GetProjectile();
            projectile.transform.position = projectileOrigin.position;
            projectile.transform.rotation = Quaternion.LookRotation(povOrigin.forward);

            Projectile projectileScript = projectile.GetComponent<Projectile>();
            if (projectileScript != null)
            {
                projectileScript.Initialize(projectilePool, povOrigin.forward, 75f, color, currentColor);
            }
        }
    }
}