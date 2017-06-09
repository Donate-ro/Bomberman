using UnityEngine;

namespace Assets.Scripts
{
    public abstract class AbstractObjectCreator
    {
        public abstract void CreateFloor();
        public abstract void CreateUnbreakableWalls();
        public abstract void CreateBreakableWalls(int countOfBreakableWalls);
        public abstract void CreatePlayer();
        public abstract void CreateEnemy();
    }
}