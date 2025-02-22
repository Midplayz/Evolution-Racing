using HyperCasual.Runner;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private BulletSpawner bulletSpawner;
    [SerializeField]
    private float speed;
    private bool startMoving;
    private float startingZPosition;
    [SerializeField]
    private float destroyBulletAfterZOffset;
   public void SetBulletSpawner(BulletSpawner bulletSpawner)
    {
        this.bulletSpawner = bulletSpawner;
    }
    private void OnEnable()
    {
        startingZPosition=transform.position.z;
        startMoving = true;
    }
   private void OnDisable()
    {
        startMoving = false;
    }
    void Update()
    {
        if(startMoving)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            if (transform.position.z > (startingZPosition + destroyBulletAfterZOffset))
            {
                startMoving = false;
                bulletSpawner.DisableBullet(this);
            }
        }
        
    }
 
}
