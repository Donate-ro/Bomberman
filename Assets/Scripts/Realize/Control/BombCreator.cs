using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    class BombCreator : Exploder
    {
        protected ResourseLoader loader;
        public float timeOfLife = 2;
        public int maxBombCount = 1;
        int bombCount = 0;
        public bool detonator;
        List<GameObject> bombsToExplode = new List<GameObject>();

        public BombCreator()
        {
            loader = new ResourseLoader();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                StartCoroutine(CreateBomb());

            if (Input.GetKeyDown(KeyCode.E))
            {
                Explode(bombsToExplode, loader.LoadExplosion(), DestroyObject);
                bombCount -= bombsToExplode.Count;
                bombsToExplode = new List<GameObject>();
            }
        }

        public IEnumerator CreateBomb()
        {
            if (maxBombCount > bombCount)
            {
                GameObject bomb = Instantiate(loader.LoadBomb(), new Vector3(transform.position.x, transform.position.y / 2, transform.position.z), new Quaternion(0, 0, 0, 0));
                bombCount++;
                yield return new WaitForSeconds(timeOfLife);
                if (!detonator)
                {
                    Explode(bomb, loader.LoadExplosion(), DestroyObject);
                    bombCount--;
                }
                else
                {
                    bombsToExplode.Add(bomb);
                }
            }
        }

        public void DestroyObject(GameObject obj)
        {
            if ((obj.CompareTag("BreakableWall")) || (obj.CompareTag("Player")) || (obj.CompareTag("Enemy")))
            {
                if (!obj.CompareTag("Enemy"))
                    PowerUp.TryToCreatePowerup(obj);
                else
                {
                    TextScript text = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<TextScript>();
                    if (obj.GetComponent<SmartAutoMovement>().isActiveAndEnabled)
                        text.AddScore(30);
                    else text.AddScore(15);
                }
                StartCoroutine(Effects.FadeEffect(obj));
            }
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Bomb")) other.isTrigger = false;
        }
    }
}
