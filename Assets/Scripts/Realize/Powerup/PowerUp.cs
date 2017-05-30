using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Assets.Scripts
{
    class PowerUp : MonoBehaviour
    {
        BombCreator bombCreator;
        PlayerControl playerControl;
        public List<Powerup> powerUps = new List<Powerup>();
        float maxSpeed = 0.15f;
        static System.Random random = new System.Random();
        static Run run;
        static ResourseLoader loader;

        private void Start()
        {
            loader = new ResourseLoader();
            run = GameObject.FindGameObjectWithTag("MainCamera").gameObject.GetComponent<Run>();
            playerControl = gameObject.GetComponent<PlayerControl>();
            bombCreator = gameObject.GetComponent<BombCreator>();
            powerUps.Add(Powerup.MoreBombs);
            powerUps.Add(Powerup.ExplosionRadius);
            RefreshCollectible();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("MoreBombs"))
            {
                AddPowerupAndHide(Powerup.MoreBombs, other);
                RefreshCollectible();
            }
            if (other.CompareTag("Speed"))
            {
                AddPowerupAndHide(Powerup.Speed, other);
                playerControl.movementSpeed = maxSpeed;
                SetActiveSkinOfPowerup("Foot");

            }
            if (other.CompareTag("ExplosionRadius"))
            {
                AddPowerupAndHide(Powerup.ExplosionRadius, other);
                RefreshCollectible();
            }
            if (other.CompareTag("WalkOnBombs"))
            {
                AddPowerupAndHide(Powerup.WalkOnBombs, other);
                SetActiveSkinOfPowerup("SkinBomb");
            }

            if (other.CompareTag("WalkOnWalls"))
            {
                AddPowerupAndHide(Powerup.WalkOnWalls, other);
                SetActiveSkinOfPowerup("BreakableWall");
            }
            if (other.CompareTag("Detonator"))
            {
                AddPowerupAndHide(Powerup.Detonator, other);
                bombCreator.detonator = true;
                bombCreator.timeOfLife = 0;
                SetActiveSkinOfPowerup("Detonator");
            }
        }

        void SetActiveSkinOfPowerup(string tag)
        {
            foreach (Transform child in transform)
                if (child.CompareTag(tag)) child.gameObject.SetActive(true);
        }

        void RefreshCollectible()
        {
            bombCreator.maxBombCount = powerUps.FindAll(s => s == Powerup.MoreBombs).Count;
            bombCreator.strengthOfExplosion = powerUps.FindAll(s => s == Powerup.ExplosionRadius).Count;
        }

        IEnumerator ShowSparks()
        {
            GameObject sparks = new GameObject();
            foreach (Transform child in transform)
                if (child.CompareTag("Sparks")) sparks = child.gameObject;
            sparks.SetActive(true);
            yield return new WaitForSeconds(2f);
            sparks.SetActive(false);
        }

        void AddPowerupAndHide(Powerup powerup, Collider other)
        {
            StartCoroutine(ShowSparks());
            StartCoroutine(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<TextScript>().ShowPowerupText(other.gameObject.tag));
            ResourseLoader loader = new ResourseLoader();
            gameObject.GetComponent<Exploder>().Explode(other.gameObject, loader.LoadExplosion());
            powerUps.Add(powerup);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (powerUps.Contains(Powerup.WalkOnWalls))
                if (collision.gameObject.tag == "BreakableWall") Physics.IgnoreCollision(gameObject.transform.GetChild(1).GetComponent<Collider>(), collision.gameObject.GetComponent<Collider>());
            if (powerUps.Contains(Powerup.WalkOnBombs))
                if (collision.gameObject.tag == "Bomb") Physics.IgnoreCollision(gameObject.transform.GetChild(1).GetComponent<Collider>(), collision.gameObject.GetComponent<Collider>());
        }

        public static void TryToCreatePowerup(GameObject obj)
        {
            int countOfBreakablleWalls = GameObject.FindGameObjectsWithTag("BreakableWall").Length;
            if (countOfBreakablleWalls > run.powerUps.Count)
            {
                if (random.Next(1, GameObject.FindGameObjectsWithTag("BreakableWall").Length) == 1)
                    RandomPowerupAndInstantiate(obj);
            }
            else RandomPowerupAndInstantiate(obj);
        }

        static void RandomPowerupAndInstantiate(GameObject obj)
        {
            Powerup powerup;
            int rand = random.Next(0, run.powerUps.Count);
            powerup = run.powerUps.ElementAt(rand);
            Instantiate(loader.LoadPowerup(powerup), obj.transform.position, new Quaternion(0, 0, 0, 0));
            run.powerUps.Remove(powerup);
        }
    }
    enum Powerup
    {
        MoreBombs,
        Speed,
        ExplosionRadius,
        Detonator,
        WalkOnWalls,
        WalkOnBombs
    }
}
