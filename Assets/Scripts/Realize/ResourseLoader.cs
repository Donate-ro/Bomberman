using UnityEngine;
namespace Assets.Scripts
{
    class ResourseLoader : AbstractReasorceLoader
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
        public override GameObject LoadExplosionEffect()
        {
            return Resources.Load("Explosion Effect") as GameObject;
        }
        public GameObject LoadExplosion()
        {
            return Resources.Load("Explosion") as GameObject;
        }


        public GameObject LoadPowerUpByPowerup(Powerup powerup)
        {
            if (powerup == Powerup.Detonator) return LoadDetonator();
            if (powerup == Powerup.ExplosionRadius) return LoadExplosionRadius();
            if (powerup == Powerup.MoreBombs) return LoadMoreBombs();
            if (powerup == Powerup.Speed) return LoadSpeed();
            if (powerup == Powerup.WalkOnBombs) return LoadWalkOnBombs();
            if (powerup == Powerup.WalkOnWalls) return LoadWalkOnWalls();
            return null;
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
        public GameObject LoadCanvas()
        {
            return Resources.Load("Text/Canvas") as GameObject;
        }
        public GameObject LoadEventSystem()
        {
            return Resources.Load("Text/EventSystem") as GameObject;
        }
    }
}
