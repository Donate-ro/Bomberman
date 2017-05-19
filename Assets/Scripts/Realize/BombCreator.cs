using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    class BombCreator : Exploder
    {
        protected ResourseLoader Loader;
        public float timeOfLife = 2;
        public int maxBombCount = 1;
        int bombCount = 0;

        public BombCreator()
        {
            Loader = new ResourseLoader();
        }

        public IEnumerator CreateBomb(Vector3 positionOfPlayer)
        {
            if (maxBombCount > bombCount)
            {
                GameObject bomb = Instantiate(Loader.LoadBomb(), positionOfPlayer, new Quaternion(0, 0, 0, 0));
                bombCount++;
                yield return new WaitForSeconds(timeOfLife);
                Explode(bomb, Loader.LoadExplosion(), DestroyObject);
                bombCount--;
            }
        }

        void DestroyObject(List<RaycastHit> hits)
        {
            foreach (var hit in hits)
            {
                if ((hit.collider.CompareTag("BreakableWall")) || (hit.collider.CompareTag("Player")) || (hit.collider.CompareTag("Enemy")))
                    hit.transform.gameObject.SetActive(false);
            }
        }
    }
}
