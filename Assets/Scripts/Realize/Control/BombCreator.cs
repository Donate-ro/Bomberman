using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    class BombCreator : MonoBehaviour
    {
        protected ResourseLoader loader;
        public Exploder exploder = new Exploder();
        Run run;
        TextScript text;
        public float timeOfLife = 2;
        public int maxBombCount = 1;
        int bombCount = 0;
        public bool detonator;
        List<GameObject> bombsToExplode = new List<GameObject>();

        
        private void Start()
        {
            GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            run = mainCamera.GetComponent<Run>();
            loader = new ResourseLoader();
            text = mainCamera.GetComponent<TextScript>();
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
                exploder.Explode(bombsToExplode, loader.LoadExplosionEffect(), DestroyObject);
                bombCount -= bombsToExplode.Count;
                bombsToExplode = new List<GameObject>();
            }
        }

        public IEnumerator CreateBomb()
        {
            if (maxBombCount > bombCount)
            {
                GameObject bomb = Instantiate(loader.LoadBomb(), new Vector3(transform.position.x, 0.25f, transform.position.z), new Quaternion(0, 0, 0, 0));
                bomb.AddComponent<AudioSource>();
                bomb.GetComponent<AudioSource>().PlayOneShot(AudioLoader.LoadBombSetup());
                bombCount++;
                yield return new WaitForSeconds(timeOfLife);
                if (!detonator)
                {
                    exploder.Explode(bomb, loader.LoadExplosionEffect(), DestroyObject);
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
                if (!run.killedEnemies.Contains(obj.GetInstanceID()))
                {
                    run.killedEnemies.Add(obj.GetInstanceID());
                    if (obj.CompareTag("BreakableWall"))
                    {
                        PowerUp.TryToCreatePowerup(obj);
                        StartCoroutine(Effects.FadeEffect(obj));
                    }
                    if (obj.CompareTag("Player"))
                    {
                        Effects.PlayerDeath(obj);
                    }
                    if (obj.CompareTag("Enemy"))
                    {
                        if (obj.transform.GetChild(1).GetComponent<SmartAutoMovement>() != null)
                            text.AddScore(30);
                        else text.AddScore(15);
                        if (GameObject.FindGameObjectsWithTag("Enemy").Length / 2 == 1)
                        {
                            gameObject.transform.GetChild(1).gameObject.GetComponent<AudioSource>().PlayOneShot(AudioLoader.LoadPlayerWin());
                            gameObject.GetComponent<Animator>().SetTrigger("Win");
                        }
                        obj.GetComponent<Animator>().SetTrigger("Death");
                        obj.transform.GetChild(1).gameObject.GetComponent<AudioSource>().PlayOneShot(AudioLoader.LoadEnemyDead());
                        StartCoroutine(Effects.FadeEffect(obj.transform.GetChild(1).gameObject));
                    }
                }
            }
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Bomb")) other.isTrigger = false;
        }
    }
}
