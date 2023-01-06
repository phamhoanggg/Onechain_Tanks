using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks
{
    public class BulletSpawner : FastSingleton<BulletSpawner>
    {
        [SerializeField] Bullet bulletPrefab;

        private Queue<Bullet> DeActiveBullets = new Queue<Bullet>();

        public void OnSpawnBullet(Vector3 pos, Vector3 direct, Color color)
        {
            if (DeActiveBullets.Count > 0)
            {
                Bullet bullet = DeActiveBullets.Dequeue();
                bullet.SetPosition(pos);
                bullet.SetDirect(direct);
                bullet.SetColor(color);
                bullet.gameObject.SetActive(true);
                bullet.OnSpawn();
            }
            else
            {
                Bullet bullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
                bullet.SetDirect(direct);
                bullet.SetColor(color);
                bullet.OnSpawn();
            }
        }

        public void OnDisableBullet(Bullet bullet)
        {
            bullet.gameObject.SetActive(false);
            DeActiveBullets.Enqueue(bullet);
        }
    }
}

