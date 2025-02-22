using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public Vector3 velocity;
    public Vector3 projectileCutoff;

    private void Update()
    {
        transform.position += velocity * Time.deltaTime;
        if (projectileCutoff.z < transform.position.z)
        {
            Destroy(gameObject);
        }
    }
}
