using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks
{
    public class PlayerTank : Tanks
    {
        [SerializeField] private Joystick movingJt, aimingJt;
        

        // Update is called once per frame
        void FixedUpdate()
        {
            if (movingJt.direction != Vector3.zero)
            {
                tankTransform.position += movingJt.direction.normalized * moveSpeed;
                tankBody.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(-movingJt.direction.x, movingJt.direction.y) * Mathf.Rad2Deg);
                tankShadow.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(-movingJt.direction.x, movingJt.direction.y) * Mathf.Rad2Deg);
            }
            
            if (currentAtkCD > 0)
            {
                currentAtkCD -= Time.fixedDeltaTime;
            }

            if (aimingJt.direction != Vector3.zero)
            {
                tankCannon.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(-aimingJt.direction.x, aimingJt.direction.y) * Mathf.Rad2Deg);
                cannonDirect = aimingJt.direction;
                tankAnimator.ResetTrigger(Cache.ANIM_NORMAL);
                tankAnimator.SetTrigger(Cache.ANIM_ATTACK);
                if (currentAtkCD <= 0)
                {
                    OnFireBullet();
                }
            }
            else
            {
                tankAnimator.ResetTrigger(Cache.ANIM_ATTACK);
                tankAnimator.SetTrigger(Cache.ANIM_NORMAL);
            }
        }
    }

}
