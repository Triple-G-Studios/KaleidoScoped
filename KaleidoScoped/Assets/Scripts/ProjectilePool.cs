using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This doesn't work flawlessly like I noticed some clicks where it wouldn't shoot. 
//It helps a ton with performance though I think cause projectiles aren't being created and destroyed every time you shoot.

public class ProjectilePool : MonoBehaviour
{
    public GameObject projectilePrefab;
    private Queue<GameObject> pool = new Queue<GameObject>();
    public int initialPoolSize = 20;

    void Start()
    {
        InitializePool();
    }

    void InitializePool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject newProjectile = Instantiate(projectilePrefab, transform);
            newProjectile.SetActive(false);
            pool.Enqueue(newProjectile);
        }
    }

    public GameObject GetProjectile()
    {
        if (pool.Count > 0)
        {
            GameObject projectile = pool.Dequeue();
            projectile.SetActive(true);
            return projectile;
        }
        else
        {
            // If the pool is empty, create a new projectile
            GameObject newProjectile = Instantiate(projectilePrefab, transform);
            newProjectile.SetActive(true);
            return newProjectile;
        }
    }

    public void ReturnProjectile(GameObject projectile)
    {
        projectile.SetActive(false);
        pool.Enqueue(projectile);
    }
}
