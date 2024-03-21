using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Kaleidoscoped
{
    public class PlayerController2 : MonoBehaviour
    {
        public static PlayerController2 instance;

        [SerializeField] private Transform CameraRoot;
        public Transform projectileOrigin;
        public GameObject projectilePrefab;
        public ProjectilePool projectilePool;
        public GameObject pauseMenuUI;
        public GameObject canvas;
        public float attackRange;

        //State Tracking
        public List<string> keyIdsObtained;
        public string currentColor = "";
        public Color color = Color.blue;
        public string weapon = "rifle";
        private bool isShooting = false;

        private void Awake()
        {
            instance = this;
            keyIdsObtained = new List<string>();
            pauseMenuUI.SetActive(false);
        }

        private void Start()
        {

        }

        void OnShoot()
        {
            GameObject projectile = projectilePool.GetProjectile();
            projectile.transform.position = projectileOrigin.position;
            projectile.transform.rotation = Quaternion.LookRotation(CameraRoot.forward);

            Projectile projectileScript = projectile.GetComponent<Projectile>();
            if (projectileScript != null)
            {
                projectileScript.Initialize(projectilePool, CameraRoot.forward, 75f, color, currentColor);
            }
        }
    }
}