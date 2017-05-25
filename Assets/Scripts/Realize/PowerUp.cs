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

        private void Start()
        {
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
            }
            if (other.CompareTag("ExplosionRadius"))
            {
                AddPowerupAndHide(Powerup.ExplosionRadius, other);
                RefreshCollectible();
            }
            if (other.CompareTag("WalkOnBombs"))
                AddPowerupAndHide(Powerup.WalkOnBombs, other);
            if (other.CompareTag("WalkOnWalls"))
                AddPowerupAndHide(Powerup.WalkOnWalls, other);
            if (other.CompareTag("Detonator"))
            {
                AddPowerupAndHide(Powerup.Detonator, other);
                bombCreator.detonator = true;
                bombCreator.timeOfLife = 0;
            }
        }

        void RefreshCollectible()
        {
            bombCreator.maxBombCount = powerUps.FindAll(s => s == Powerup.MoreBombs).Count;
            bombCreator.strengthOfExplosion = powerUps.FindAll(s => s == Powerup.ExplosionRadius).Count;
        }

        void AddPowerupAndHide(Powerup powerup, Collider other)
        {
            powerUps.Add(powerup);
            other.gameObject.SetActive(false);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (powerUps.Contains(Powerup.WalkOnWalls))
                if (collision.gameObject.tag == "BreakableWall") Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), collision.gameObject.GetComponent<Collider>());
            if (powerUps.Contains(Powerup.WalkOnBombs))
                if (collision.gameObject.tag == "Bomb") Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), collision.gameObject.GetComponent<Collider>());
        }

        public static void TryToCreatePowerup(GameObject obj)
        {
            System.Random random = new System.Random();
            ResourseLoader loader = new ResourseLoader();
            Powerup powerup;
            Run run = GameObject.FindGameObjectWithTag("MainCamera").gameObject.GetComponent<Run>();
            if (random.Next(1, GameObject.FindGameObjectsWithTag("BreakableWall").Length) == 1)
            {
                powerup = run.powerUps.ElementAt(random.Next(0, run.powerUps.Count));
                Instantiate(loader.LoadPowerUpByPowerup(powerup), obj.transform.position, new Quaternion(0, 0, 0, 0));
                run.powerUps.Remove(powerup);
            }
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
