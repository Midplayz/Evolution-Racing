using HyperCasual.Runner;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class BulletSpawner : MonoBehaviour
{
    private ObjectPool<Bullet> bulletPool;
    [SerializeField]
    private Bullet bulletPrefab;

    [Range(0.001f, 1f),SerializeField]
    private float fireRate;

    bool keepSpawningBullets = false;

 
    private void OnEnable()
    {
        if(bulletPool==null)
        {
            bulletPool = new ObjectPool<Bullet>(createFunc: SpawnBullet, actionOnGet: GetBullet, actionOnRelease: ReleaseBullet, actionOnDestroy: DestroyBullet, defaultCapacity: 10000, maxSize: 20000);
        }
        keepSpawningBullets = true;
        StartCoroutine(StartSpawning());
    }
    private void OnDisable()
    {
        keepSpawningBullets = false;
    }
    IEnumerator StartSpawning()
    {
        while (keepSpawningBullets)
        {
            bulletPool.Get();
            yield return new WaitForSeconds(fireRate);
        }
    }
    public void DisableBullet(Bullet bullet)
    {
        bulletPool.Release(bullet);
    }
    #region PoolFunctionForBullet
    private Bullet SpawnBullet()
    {
        Bullet bulletGameObject = Instantiate(bulletPrefab);
        bulletGameObject.gameObject.SetActive(false);
        bulletGameObject.SetBulletSpawner(this);
        return bulletGameObject;
    }
    private void GetBullet(Bullet bullet)
    {
        bullet.transform.parent = this.transform;
        Vector3 _pos = Vector3.zero;
        bullet.transform.localPosition = Vector3.zero;
        bullet.transform.parent = GameManager.Instance.bulletParent.transform;
        bullet.gameObject.SetActive(true);
    }
    private void ReleaseBullet(Bullet bullet)
    {

        bullet.gameObject.SetActive(false);
        bullet.transform.position = Vector3.zero;
    }
    private void DestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }
    #endregion
}
