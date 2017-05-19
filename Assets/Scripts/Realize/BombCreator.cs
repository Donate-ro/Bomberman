using System;
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

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space))
                StartCoroutine(CreateBomb());
        }

        public IEnumerator CreateBomb()
        {
            if (maxBombCount > bombCount)
            {
                GameObject bomb = Instantiate(Loader.LoadBomb(), new Vector3(transform.position.x, transform.position.y / 2, transform.position.z), new Quaternion(0, 0, 0, 0));
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
                    StartCoroutine(Effects.FadeEffect(hit.transform.gameObject));
            }
        }
    }
}
