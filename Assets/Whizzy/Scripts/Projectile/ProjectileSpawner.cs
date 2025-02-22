using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject projectile;
    public Vector3 projectileOriginOffset;
    public float projectileDistance = 50;
    public float lastSpawnedTime = 0;
    public float spawnInterval = 0.5f;
    public float projectileSpeed = 50;

    private void Update()
    {

        if (lastSpawnedTime + spawnInterval < Time.time)
        {
            lastSpawnedTime = Time.time;

            GameObject projectileInstance = GameObject.Instantiate(projectile);
            projectileInstance.transform.position = transform.position + projectileOriginOffset;
            projectileInstance.GetComponent<ProjectileBehaviour>().velocity = transform.forward;
            projectileInstance.GetComponent<ProjectileBehaviour>().velocity.Normalize();
            projectileInstance.GetComponent<ProjectileBehaviour>().velocity *= projectileSpeed;
            projectileInstance.GetComponent<ProjectileBehaviour>().projectileCutoff = projectileInstance.transform.position
                + (transform.forward.normalized * projectileDistance);
            projectileInstance.SetActive(true);

        }
    }
}
