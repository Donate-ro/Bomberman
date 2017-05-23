using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts
{
    public class ResourseLoader : AbstractReasorceLoader
    {
        public override GameObject LoadFloor()
        {
            return Resources.Load("Floor") as GameObject;
        }
        public override GameObject LoadUnbreakableWall()
        {
            return Resources.Load("Walls/Unbreakable Wall") as GameObject;
        }
        public override GameObject LoadBreakableWall()
        {
            return Resources.Load("Walls/Breakable Wall") as GameObject;
        }

        public override GameObject LoadPlayer()
        {
            return Resources.Load("Players/Player") as GameObject;
        }
        public override GameObject LoadEnemy()
        {
            return Resources.Load("Players/Enemy") as GameObject;
        }
        public override GameObject LoadBomb()
        {
            return Resources.Load("Bomb") as GameObject;
        }
        public override GameObject LoadExplosion()
        {
            return Resources.Load("Explosion Effect") as GameObject;
        }
        public List<GameObject> LoadPowerUps()
        {
            List<GameObject> powerups = new List<GameObject>();
            powerups.Add(LoadDetonator());
            powerups.Add(LoadExplosionRadius());
            powerups.Add(LoadMoreBombs());
            powerups.Add(LoadSpeed());
            powerups.Add(LoadWalkOnBombs());
            powerups.Add(LoadWalkOnWalls());
            return powerups;
        }
        public GameObject LoadDetonator()
        {
            return Resources.Load("Powerup/Detonator") as GameObject;
        }
        public GameObject LoadExplosionRadius()
        {
            return Resources.Load("Powerup/Explosion Radius") as GameObject;
        }
        public GameObject LoadMoreBombs()
        {
            return Resources.Load("Powerup/More Bombs") as GameObject;
        }
        public GameObject LoadSpeed()
        {
            return Resources.Load("Powerup/Speed") as GameObject;
        }
        public GameObject LoadWalkOnBombs()
        {
            return Resources.Load("Powerup/Walk On Bombs") as GameObject;
        }
        public GameObject LoadWalkOnWalls()
        {
            return Resources.Load("Powerup/Walk On Walls") as GameObject;
        }
    }
}
