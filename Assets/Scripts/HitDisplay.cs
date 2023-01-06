using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks
{
    public class HitDisplay : FastSingleton<HitDisplay>
    {
        [SerializeField] private GameObject VFXPref;
        private Queue<GameObject> DeActiveVFX = new Queue<GameObject>();
        
        public void OnDisplayVFX(Vector3 pos)
        {
            GameObject vfx;
            if (DeActiveVFX.Count > 0)
            {
                vfx = DeActiveVFX.Dequeue();
                vfx.transform.position = pos;
                vfx.SetActive(true);
            }
            else
            {
                vfx = Instantiate(VFXPref, pos, Quaternion.identity);
            }

            StartCoroutine(OnDisableVFX(vfx));
        }

        private IEnumerator OnDisableVFX(GameObject vfx)
        {
            yield return new WaitForSeconds(0.1f);
            vfx.SetActive(false);
            DeActiveVFX.Enqueue(vfx);
        }
        
    }
}

