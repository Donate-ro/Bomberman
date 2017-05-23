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
    }
}
