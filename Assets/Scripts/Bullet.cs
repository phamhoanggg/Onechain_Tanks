using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private Transform bulletTf;
        private Vector3 direct;
        [SerializeField] private SpriteRenderer spriteRenderer;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            bulletTf.position += direct.normalized * moveSpeed;
        }

        public void OnSpawn()
        {
            bulletTf.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(-direct.x, direct.y) * Mathf.Rad2Deg);
        }

        public void SetPosition(Vector3 pos)
        {
            bulletTf.position = pos;
        }

        public void SetDirect(Vector3 direct)
        {
            this.direct = direct;
        }

        public void SetColor(Color color)
        {
            spriteRenderer.color = color;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Cache.TAG_REDTANK))
            {
                Tanks_GameManager.instance.DecreaseScore(Cache.TAG_REDTANK);
                Tanks_GameManager.instance.OnShakeCamera();
            }
            else if (other.CompareTag(Cache.TAG_BLUETANK))
            {
                Tanks_GameManager.instance.DecreaseScore(Cache.TAG_BLUETANK);
                Tanks_GameManager.instance.OnShakeCamera();
            }
            DisplayVFX();
            BulletSpawner.instance.OnDisableBullet(this);
        }


        private void DisplayVFX()
        {
            HitDisplay.instance.OnDisplayVFX(bulletTf.position);
        }
    }

}
