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

        public IEnumerator DestroyWithEffect(GameObject hit)
        {
            Color tmpColor = hit.GetComponent<Renderer>().material.color;
            while (tmpColor.a > 0)
            {
                tmpColor.a -= 0.1f;
                hit.GetComponent<Renderer>().material.color = tmpColor;
                yield return new WaitForSeconds(0.01f);
            }
            hit.SetActive(false);
        }

        void DestroyObject(List<RaycastHit> hits)
        {
            foreach (var hit in hits)
            {
                try
                {
                    if ((hit.collider.CompareTag("BreakableWall")) || (hit.collider.CompareTag("Player")) || (hit.collider.CompareTag("Enemy")))
                    {
                        //Color tmpColor = hit.transform.gameObject.GetComponent<Renderer>().material.color;
                        //tmpColor.a -= 0.25f;
                        //hit.transform.gameObject.GetComponent<Renderer>().material.color = tmpColor;
                        IEnumerator tmp = DestroyWithEffect(hit.transform.gameObject);
                        StartCoroutine(tmp);
                        //hit.transform.gameObject.SetActive(false);
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
}
