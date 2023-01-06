using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks
{
    public class Tanks : MonoBehaviour
    {
        [SerializeField] protected float moveSpeed;
        [SerializeField] protected Transform tankBody, tankShadow, tankCannon, tankTransform;
        [SerializeField] protected float atkCD, currentAtkCD;
        [SerializeField] protected Transform bullletSpawnPos;
        [SerializeField] protected Animator tankAnimator;
        [SerializeField] protected SpriteRenderer spriteRenderer;
        
        protected Vector3 cannonDirect;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        protected void OnFireBullet()
        {
            BulletSpawner.instance.OnSpawnBullet(bullletSpawnPos.position, cannonDirect, spriteRenderer.color);
            currentAtkCD = atkCD;
        }

        public void SetPosition(Vector3 pos)
        {
            tankTransform.position = pos;
        }


    }
}

