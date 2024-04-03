using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using Mirror;

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
        public string weapon = "rifle";
        public int teamNumber = 0;

        public GameObject pauseMenuUI;

        private bool isPaused = false;


        public GameObject playerCamera;
        public GameObject PlayerFollowCamera;
        public GameObject canvas;
        public GameObject CameraRoot;
        public GameObject EventSystem;
        public GameObject Player;


        void Awake()
        {
            /*if (!isLocalPlayer)
            {
                return;
            }*/
            instance = this;
            keyIdsObtained = new List<string>();
            pauseMenuUI.SetActive(false);
            loadData();
        }

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            /*if (!isLocalPlayer)
            {
                playerCamera.SetActive(false);
                PlayerFollowCamera.SetActive(true);
                canvas.SetActive(false);
                CameraRoot.SetActive(false);
                EventSystem.SetActive(false);
                // Player.GetComponent<PlayerInput>().enabled = false;
                Player.GetComponent<PlayerController>().enabled = false;
                // Player.GetComponent<FirstPersonController>().enabled = false;
                Player.GetComponent<CharacterController>().enabled = false;
            }*/
        }



        void Update()
        {
            /*if (!isLocalPlayer)
            {
                playerCamera.SetActive(false);
                PlayerFollowCamera.SetActive(false);
                canvas.SetActive(false);
                CameraRoot.SetActive(false);
                EventSystem.SetActive(false);
                // Player.GetComponent<PlayerInput>().enabled = false;
                Player.GetComponent<PlayerController>().enabled = false;
                // Player.GetComponent<FirstPersonController>().enabled = false;
                Player.GetComponent<CharacterController>().enabled = false;
                return;
            }*/
            // GameObject popup = PopUpController.GetComponent<popupmenu>(); 

            if (Input.GetKeyDown(KeyCode.Escape))
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

            float moveX = Input.GetAxis("Horizontal") * Time.deltaTime * 110.0f;
            float moveZ = Input.GetAxis("Vertical") * Time.deltaTime * 4f;

            transform.Rotate(0, moveX, 0);
            transform.Translate(0, 0, moveZ);

            if (Input.GetMouseButtonDown(0))
            {
                OnPrimaryAttack();
            }
        }

        public void PauseGame()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void ResumeGame()
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;

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
            /*if (!isLocalPlayer)
            {
                return;
            }*/

            if (weapon == "shotgun")
            {
                GameObject projectile1 = projectilePool.GetProjectile();
                projectile1.transform.position = projectileOrigin.position;
                projectile1.transform.rotation = Quaternion.LookRotation(povOrigin.forward) * Quaternion.Euler(Vector3.forward * 10);

                GameObject projectile2 = projectilePool.GetProjectile();
                projectile2.transform.position = projectileOrigin.position;
                projectile2.transform.rotation = Quaternion.LookRotation(povOrigin.forward);

                GameObject projectile3 = projectilePool.GetProjectile();
                projectile3.transform.position = projectileOrigin.position;
                projectile3.transform.rotation = Quaternion.LookRotation(povOrigin.forward) * Quaternion.Euler(Vector3.forward * -10);

                Projectile projectileScript1 = projectile1.GetComponent<Projectile>();
                Projectile projectileScript2 = projectile2.GetComponent<Projectile>();
                Projectile projectileScript3 = projectile3.GetComponent<Projectile>();

                if (projectileScript1 != null && projectileScript2 != null && projectileScript3 != null)
                {
                    //projectileScript1.Initialize(projectilePool, Quaternion.AngleAxis(10, Vector3.up) * povOrigin.forward, 15f, color, currentColor);
                    //projectileScript2.Initialize(projectilePool, povOrigin.forward, 15f, color, currentColor);
                    //projectileScript3.Initialize(projectilePool, Quaternion.AngleAxis(-10, Vector3.up) * povOrigin.forward, 15f, color, currentColor);
                }
            }
            if (weapon == "rifle")
            {
                GameObject projectile = projectilePool.GetProjectile();
                projectile.transform.position = projectileOrigin.position;
                projectile.transform.rotation = Quaternion.LookRotation(povOrigin.forward);

                Projectile projectileScript = projectile.GetComponent<Projectile>();

                //if (projectileScript != null) projectileScript.Initialize(projectilePool, povOrigin.forward, 75f, color, currentColor);
            }

        }

        //load the player's info into the scene proper
        void loadData()
        {
            currentColor = PlayerPrefs.GetString("color");
            weapon = PlayerPrefs.GetString("weapon");
            teamNumber = PlayerPrefs.GetInt("team");

            PlayerPrefs.DeleteAll();

            switch (currentColor.ToLower())
            {
                case "purple":
                    color = new Color(1f, 0f, 1f);
                    currentColor = "purple";
                    break;
                case "red":
                    color = Color.red;
                    currentColor = "red";
                    break;
                case "green":
                    color = Color.green;
                    currentColor = "green";
                    break;
                case "blue":
                    color = Color.blue;
                    currentColor = "blue";
                    break;
            }

            var paintballRenderer = projectilePrefab.GetComponent<Renderer>();
            paintballRenderer.sharedMaterial.SetColor("_Color", color);
        }
    }
}