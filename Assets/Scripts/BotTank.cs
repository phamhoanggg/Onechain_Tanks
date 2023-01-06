using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks
{
    public class BotTank : Tanks
    {
        [SerializeField] private Transform target;

        [SerializeField] private Transform[] Destination;

        private Transform currentDes;
        private Vector3 direct;

        private void Start()
        {

            tankAnimator.SetTrigger(Cache.ANIM_ATTACK);
        }
        // Update is called once per frame
        void Update()
        {
            
            cannonDirect = target.position - tankTransform.position;
            tankCannon.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(-cannonDirect.x, cannonDirect.y) * Mathf.Rad2Deg);
            if (currentAtkCD > 0)
            {
                currentAtkCD -= Time.deltaTime;
            }
            else
            {
                OnFireBullet();
            }

            if(currentDes == null)
            {
                currentDes = Destination[Random.Range(0, Destination.Length)];
            }

            else
            {
                direct = currentDes.position - tankTransform.position;

                if (Vector2.Distance(tankTransform.position, currentDes.position) < 0.1f)
                {
                    currentDes = null;
                }
            }
        }

        private void FixedUpdate()
        {
            if (currentDes != null)
            {
                tankTransform.position += direct.normalized * moveSpeed;
                tankBody.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(-direct.x, direct.y) * Mathf.Rad2Deg);
                tankShadow.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(-direct.x, direct.y) * Mathf.Rad2Deg);
            }
        }


    }
}

