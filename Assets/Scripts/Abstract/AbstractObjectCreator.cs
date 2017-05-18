using UnityEngine;

namespace Assets.Scripts
{
    public abstract class AbstractObjectCreator : MonoBehaviour
    {
        public abstract void CreateFloor();
        public abstract void CreateUnbreakableWalls();
        public abstract void CreateBreakableWalls(int countOfBreakableWalls);
        abstract public void CreatePlayer();
        abstract public void CreateEnemy();
    }
}