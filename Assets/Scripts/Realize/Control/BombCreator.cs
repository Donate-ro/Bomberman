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
            {
                gameObject.GetComponent<Animator>().SetTrigger("SetOrGet");
                StartCoroutine(CreateBomb());
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                Explode(bombsToExplode, loader.LoadExplosionEffect(), DestroyObject);
                bombCount -= bombsToExplode.Count;
                bombsToExplode = new List<GameObject>();
            }
        }

        public IEnumerator CreateBomb()
        {
            if (maxBombCount > bombCount)
            {
                GameObject bomb = Instantiate(loader.LoadBomb(), new Vector3(transform.position.x, 0.5f, transform.position.z), new Quaternion(0, 0, 0, 0));
                bombCount++;
                yield return new WaitForSeconds(timeOfLife);
                if (!detonator)
                {
                    Explode(bomb, loader.LoadExplosionEffect(), DestroyObject);
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
                if (obj.CompareTag("BreakableWall"))
                    PowerUp.TryToCreatePowerup(obj);
                if (obj.CompareTag("Player"))
                {
                    gameObject.GetComponent<Animator>().SetTrigger("Death");
                    StartCoroutine(Effects.FadeEffect(obj.transform.GetChild(1).gameObject));
                }
                if (obj.CompareTag("Enemy"))
                {
                    TextScript text = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<TextScript>();
                    if (obj.transform.GetChild(1).GetComponent<SmartAutoMovement>()!=null)
                        text.AddScore(30);
                    else text.AddScore(15);
                    obj.GetComponent<Animator>().SetTrigger("Death");
                    StartCoroutine(Effects.FadeEffect(obj.transform.GetChild(1).gameObject));
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
